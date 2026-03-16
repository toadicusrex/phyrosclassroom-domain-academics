using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default;

public sealed class InMemoryAcademicHydratedModelCache : IAcademicHydratedModelCache
{
    private readonly Dictionary<Guid, AcademicAggregate> _academics = [];
    private readonly object _gate = new();

    public Task<AcademicAggregate?> GetAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        lock (_gate)
        {
            _academics.TryGetValue(academicId, out var academic);
            return Task.FromResult(academic);
        }
    }

    public Task SetAsync(AcademicAggregate academic, CancellationToken cancellationToken = default)
    {
        lock (_gate)
        {
            _academics[academic.AcademicId] = academic;
        }

        return Task.CompletedTask;
    }
}
