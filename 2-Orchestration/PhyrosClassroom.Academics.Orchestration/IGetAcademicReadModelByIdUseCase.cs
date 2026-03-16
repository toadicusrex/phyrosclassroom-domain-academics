using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IGetAcademicReadModelByIdUseCase
{
    Task<AcademicReadModel?> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default);
}
