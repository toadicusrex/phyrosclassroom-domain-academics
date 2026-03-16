using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence;

public interface IAcademicHydratedModelCache
{
    Task<AcademicAggregate?> GetAsync(Guid academicId, CancellationToken cancellationToken = default);

    Task SetAsync(AcademicAggregate academic, CancellationToken cancellationToken = default);
}
