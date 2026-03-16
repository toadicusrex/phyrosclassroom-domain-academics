using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence;

public interface IAcademicReadModelStore
{
    Task UpsertAsync(AcademicReadModel academic, CancellationToken cancellationToken = default);

    Task<AcademicReadModel?> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default);

    Task<AcademicReadModel?> GetBySourceReferenceAsync(string sourceSystem, string sourceReference, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AcademicReadModel>> ListAsync(CancellationToken cancellationToken = default);
}
