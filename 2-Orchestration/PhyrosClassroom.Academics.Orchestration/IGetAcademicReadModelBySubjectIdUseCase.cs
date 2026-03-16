using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IGetAcademicReadModelBySubjectIdUseCase
{
    Task<AcademicReadModel?> ExecuteAsync(string subjectId, CancellationToken cancellationToken = default);
}
