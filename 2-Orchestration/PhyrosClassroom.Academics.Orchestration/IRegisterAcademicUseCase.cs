using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IRegisterAcademicUseCase
{
    Task<AcademicAggregate> ExecuteAsync(RegisterAcademicRequest request, CancellationToken cancellationToken = default);
}
