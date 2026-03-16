using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IGetAcademicAtPointInTimeUseCase
{
    Task<AcademicAggregate?> ExecuteAsync(Guid academicId, DateTimeOffset pointInTimeUtc, CancellationToken cancellationToken = default);
}
