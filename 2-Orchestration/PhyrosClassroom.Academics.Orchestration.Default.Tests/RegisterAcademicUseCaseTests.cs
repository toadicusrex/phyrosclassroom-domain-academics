using PhyrosClassroom.Academics.Engines;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;
using PhyrosClassroom.Academics.Orchestration;
using PhyrosClassroom.Academics.Orchestration.Default;

namespace PhyrosClassroom.Academics.Orchestration.Default.Tests;

public sealed class RegisterAcademicUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_PersistsAcademicAndHydratesCache()
    {
        var eventStore = new FakeAcademicEventStore();
        var readModelStore = new FakeAcademicReadModelStore();
        var hydratedModelCache = new FakeAcademicHydratedModelCache();
        var useCase = new RegisterAcademicUseCase(
            new StubAcademicCodeGenerator(),
            eventStore,
            readModelStore,
            hydratedModelCache);

        var academic = await useCase.ExecuteAsync(
            new RegisterAcademicRequest("Alice", "Bennett"));

        var storedEvents = await eventStore.GetByIdAsync(academic.AcademicId);
        var storedReadModel = await readModelStore.GetByIdAsync(academic.AcademicId);
        var cachedAcademic = await hydratedModelCache.GetAsync(academic.AcademicId);

        Assert.Single(storedEvents);
        Assert.NotNull(storedReadModel);
        Assert.NotNull(cachedAcademic);
        Assert.Equal("AB-REFERENCE", academic.AcademicCode);
        Assert.Equal("Alice", storedReadModel!.GivenName);
    }

    [Fact]
    public async Task ExecuteAsync_RehydratesFromCacheBeforeReadingEventStore()
    {
        var cachedAcademic = AcademicAggregate.Register(
            Guid.NewGuid(),
            "AB-REFERENCE",
            "Alice",
            "Bennett",
            new DateTimeOffset(2026, 2, 28, 8, 0, 0, TimeSpan.Zero));

        var eventStore = new FakeAcademicEventStore();
        var hydratedModelCache = new FakeAcademicHydratedModelCache();
        await hydratedModelCache.SetAsync(cachedAcademic);

        var useCase = new GetAcademicByIdUseCase(eventStore, hydratedModelCache);

        var academic = await useCase.ExecuteAsync(cachedAcademic.AcademicId);

        Assert.NotNull(academic);
        Assert.Equal(cachedAcademic.AcademicId, academic!.AcademicId);
        Assert.Equal(0, eventStore.ReadCount);
    }

    private sealed class StubAcademicCodeGenerator : IAcademicCodeGenerator
    {
        public string GenerateCode(string givenName, string familyName, DateTimeOffset occurredUtc)
        {
            return "AB-REFERENCE";
        }
    }

    private sealed class FakeAcademicEventStore : IAcademicEventStore
    {
        private readonly Dictionary<Guid, List<AcademicEventRecord>> _events = [];

        public int ReadCount { get; private set; }

        public Task AppendAsync(Guid academicId, IReadOnlyList<AcademicEventRecord> events, CancellationToken cancellationToken = default)
        {
            if (!_events.TryGetValue(academicId, out var eventList))
            {
                eventList = [];
                _events[academicId] = eventList;
            }

            eventList.AddRange(events);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<AcademicEventRecord>> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default)
        {
            ReadCount++;
            return Task.FromResult<IReadOnlyList<AcademicEventRecord>>(
                _events.TryGetValue(academicId, out var eventList) ? eventList : []);
        }
    }

    private sealed class FakeAcademicReadModelStore : IAcademicReadModelStore
    {
        private readonly Dictionary<Guid, AcademicReadModel> _academics = [];

        public Task UpsertAsync(AcademicReadModel academic, CancellationToken cancellationToken = default)
        {
            _academics[academic.AcademicId] = academic;
            return Task.CompletedTask;
        }

        public Task<AcademicReadModel?> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default)
        {
            _academics.TryGetValue(academicId, out var academic);
            return Task.FromResult(academic);
        }

        public Task<AcademicReadModel?> GetBySourceReferenceAsync(string sourceSystem, string sourceReference, CancellationToken cancellationToken = default)
        {
            var academic = _academics.Values.FirstOrDefault(item =>
                string.Equals(item.SourceSystem, sourceSystem, StringComparison.Ordinal) &&
                string.Equals(item.SourceReference, sourceReference, StringComparison.Ordinal));
            return Task.FromResult(academic);
        }

        public Task<IReadOnlyList<AcademicReadModel>> ListAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<AcademicReadModel>>(_academics.Values.ToList());
        }
    }

    private sealed class FakeAcademicHydratedModelCache : IAcademicHydratedModelCache
    {
        private readonly Dictionary<Guid, AcademicAggregate> _academics = [];

        public Task<AcademicAggregate?> GetAsync(Guid academicId, CancellationToken cancellationToken = default)
        {
            _academics.TryGetValue(academicId, out var academic);
            return Task.FromResult(academic);
        }

        public Task SetAsync(AcademicAggregate academic, CancellationToken cancellationToken = default)
        {
            _academics[academic.AcademicId] = academic;
            return Task.CompletedTask;
        }
    }
}
