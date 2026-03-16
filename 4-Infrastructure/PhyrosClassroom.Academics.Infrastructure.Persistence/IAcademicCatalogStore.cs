using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence;

public interface IAcademicCatalogStore
{
    Task<IReadOnlyList<CourseCatalogEntry>> ListCoursesAsync(CancellationToken cancellationToken = default);
    Task<CourseCatalogEntry> SaveCourseAsync(CourseCatalogEntry course, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CourseworkAssignment>> ListAssignmentsAsync(CancellationToken cancellationToken = default);
    Task<CourseworkAssignment> SaveAssignmentAsync(CourseworkAssignment assignment, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudentAcademicRecord>> ListRecordsAsync(CancellationToken cancellationToken = default);
    Task<StudentAcademicRecord?> GetRecordByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<StudentAcademicRecord> SaveRecordAsync(StudentAcademicRecord record, CancellationToken cancellationToken = default);
}
