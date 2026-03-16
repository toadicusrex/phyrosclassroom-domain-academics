using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IUpdateAcademicProfileUseCase
{
    Task<AcademicAggregate> ExecuteAsync(UpdateAcademicProfileRequest request, CancellationToken cancellationToken = default);
}
