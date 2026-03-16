using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Models.Tests;

public sealed class AcademicAggregateTests
{
    [Fact]
    public void Register_CreatesInitialEventAndReadModel()
    {
        var academicId = Guid.NewGuid();
        var occurredUtc = new DateTimeOffset(2026, 2, 28, 8, 0, 0, TimeSpan.Zero);

        var aggregate = AcademicAggregate.Register(
            academicId,
            "AB-20260228080000",
            "Alice",
            "Bennett",
            occurredUtc);

        var readModel = aggregate.ToReadModel();

        Assert.Equal(academicId, aggregate.AcademicId);
        Assert.Single(aggregate.Events);
        Assert.Equal("AcademicRegistered", aggregate.Events[0].EventType);
        Assert.Equal("Alice", readModel.GivenName);
        Assert.Equal("Bennett", readModel.FamilyName);
        Assert.Equal(occurredUtc, readModel.RegisteredAtUtc);
    }

    [Fact]
    public void RehydrateAt_ReturnsNull_WhenNoEventsExistBeforePointInTime()
    {
        var eventHistory = new[]
        {
            new AcademicEventRecord(
                Guid.NewGuid(),
                "AcademicRegistered",
                new DateTimeOffset(2026, 2, 28, 10, 0, 0, TimeSpan.Zero),
                "AB-20260228100000",
                "Alice",
                "Bennett"),
        };

        var aggregate = AcademicAggregate.RehydrateAt(
            eventHistory,
            new DateTimeOffset(2026, 2, 28, 9, 0, 0, TimeSpan.Zero));

        Assert.Null(aggregate);
    }
}
