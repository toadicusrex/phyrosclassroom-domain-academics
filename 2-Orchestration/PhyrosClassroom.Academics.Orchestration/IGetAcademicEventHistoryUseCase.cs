using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IGetAcademicEventHistoryUseCase
{
    Task<IReadOnlyList<AcademicEventRecord>> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default);
}
