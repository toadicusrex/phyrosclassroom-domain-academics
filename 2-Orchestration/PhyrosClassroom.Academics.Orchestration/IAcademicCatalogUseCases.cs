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

public interface IListAcademicSectionsUseCase
{
    Task<IReadOnlyList<CourseSection>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface ISaveAcademicSectionUseCase
{
    Task<CourseSection> ExecuteAsync(SaveCourseSectionRequest request, CancellationToken cancellationToken = default);
}

public interface IListSectionRosterEntriesUseCase
{
    Task<IReadOnlyList<SectionRosterEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default);
}

public interface ISaveSectionRosterEntryUseCase
{
    Task<SectionRosterEntry> ExecuteAsync(SaveSectionRosterEntryRequest request, CancellationToken cancellationToken = default);
}

public interface IListGradebookEntriesUseCase
{
    Task<IReadOnlyList<GradebookEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default);
}

public interface ISaveGradebookEntryUseCase
{
    Task<GradebookEntry> ExecuteAsync(SaveGradebookEntryRequest request, CancellationToken cancellationToken = default);
}

public interface IListAttendanceEntriesUseCase
{
    Task<IReadOnlyList<AttendanceEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default);
}

public interface ISaveAttendanceEntryUseCase
{
    Task<AttendanceEntry> ExecuteAsync(SaveAttendanceEntryRequest request, CancellationToken cancellationToken = default);
}

public interface IListAssignmentSubmissionsUseCase
{
    Task<IReadOnlyList<AssignmentSubmission>> ExecuteAsync(Guid sectionId, Guid assignmentId, CancellationToken cancellationToken = default);
}

public interface ISaveAssignmentSubmissionUseCase
{
    Task<AssignmentSubmission> ExecuteAsync(SaveAssignmentSubmissionRequest request, CancellationToken cancellationToken = default);
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
