using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class GetAcademicEventHistoryUseCase(IAcademicEventStore eventStore) : IGetAcademicEventHistoryUseCase
{
    public Task<IReadOnlyList<AcademicEventRecord>> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        return eventStore.GetByIdAsync(academicId, cancellationToken);
    }
}
