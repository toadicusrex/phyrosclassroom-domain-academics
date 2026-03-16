using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IGetAcademicByIdUseCase
{
    Task<AcademicAggregate?> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default);
}
