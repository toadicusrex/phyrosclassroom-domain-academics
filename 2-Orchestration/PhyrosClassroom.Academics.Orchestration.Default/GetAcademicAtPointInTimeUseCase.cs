using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class GetAcademicAtPointInTimeUseCase(IAcademicEventStore eventStore) : IGetAcademicAtPointInTimeUseCase
{
    public async Task<AcademicAggregate?> ExecuteAsync(
        Guid academicId,
        DateTimeOffset pointInTimeUtc,
        CancellationToken cancellationToken = default)
    {
        var eventHistory = await eventStore.GetByIdAsync(academicId, cancellationToken);
        return AcademicAggregate.RehydrateAt(eventHistory, pointInTimeUtc);
    }
}
