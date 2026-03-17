using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence;

public interface IAcademicCatalogStore
{
    Task<IReadOnlyList<CourseCatalogEntry>> ListCoursesAsync(CancellationToken cancellationToken = default);
    Task<CourseCatalogEntry> SaveCourseAsync(CourseCatalogEntry course, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CourseworkAssignment>> ListAssignmentsAsync(CancellationToken cancellationToken = default);
    Task<CourseworkAssignment> SaveAssignmentAsync(CourseworkAssignment assignment, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CourseSection>> ListSectionsAsync(CancellationToken cancellationToken = default);
    Task<CourseSection> SaveSectionAsync(CourseSection section, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SectionRosterEntry>> ListRosterEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default);
    Task<SectionRosterEntry> SaveRosterEntryAsync(SectionRosterEntry entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GradebookEntry>> ListGradebookEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default);
    Task<GradebookEntry> SaveGradebookEntryAsync(GradebookEntry entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AttendanceEntry>> ListAttendanceEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default);
    Task<AttendanceEntry> SaveAttendanceEntryAsync(AttendanceEntry entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AssignmentSubmission>> ListAssignmentSubmissionsAsync(Guid sectionId, Guid assignmentId, CancellationToken cancellationToken = default);
    Task<AssignmentSubmission> SaveAssignmentSubmissionAsync(AssignmentSubmission entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudentAcademicRecord>> ListRecordsAsync(CancellationToken cancellationToken = default);
    Task<StudentAcademicRecord?> GetRecordByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<StudentAcademicRecord> SaveRecordAsync(StudentAcademicRecord record, CancellationToken cancellationToken = default);
}
