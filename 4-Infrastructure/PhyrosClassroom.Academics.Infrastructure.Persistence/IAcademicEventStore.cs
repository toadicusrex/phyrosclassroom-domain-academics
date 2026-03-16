using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence;

public interface IAcademicEventStore
{
    Task AppendAsync(Guid academicId, IReadOnlyList<AcademicEventRecord> events, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AcademicEventRecord>> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default);
}
