using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IListAcademicCoursesUseCase
{
    Task<IReadOnlyList<CourseCatalogEntry>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface ISaveAcademicCourseUseCase
{
    Task<CourseCatalogEntry> ExecuteAsync(SaveCourseCatalogEntryRequest request, CancellationToken cancellationToken = default);
}

public interface IListAcademicAssignmentsUseCase
{
    Task<IReadOnlyList<CourseworkAssignment>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface ISaveAcademicAssignmentUseCase
{
    Task<CourseworkAssignment> ExecuteAsync(SaveCourseworkAssignmentRequest request, CancellationToken cancellationToken = default);
}

public interface IListStudentAcademicRecordsUseCase
{
    Task<IReadOnlyList<StudentAcademicRecord>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IGetStudentAcademicRecordByStudentIdUseCase
{
    Task<StudentAcademicRecord?> ExecuteAsync(Guid studentId, CancellationToken cancellationToken = default);
}

public interface ISaveStudentAcademicRecordUseCase
{
    Task<StudentAcademicRecord> ExecuteAsync(SaveStudentAcademicRecordRequest request, CancellationToken cancellationToken = default);
}
