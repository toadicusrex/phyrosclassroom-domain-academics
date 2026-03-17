using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;
using PhyrosClassroom.Academics.Orchestration;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class ListAcademicCoursesUseCase(IAcademicCatalogStore store) : IListAcademicCoursesUseCase
{
    public Task<IReadOnlyList<CourseCatalogEntry>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListCoursesAsync(cancellationToken);
}

public sealed class SaveAcademicCourseUseCase(IAcademicCatalogStore store) : ISaveAcademicCourseUseCase
{
    public Task<CourseCatalogEntry> ExecuteAsync(SaveCourseCatalogEntryRequest request, CancellationToken cancellationToken = default) =>
        store.SaveCourseAsync(
            new CourseCatalogEntry
            {
                CourseId = request.CourseId,
                CourseCode = request.CourseCode.Trim(),
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Department = request.Department.Trim(),
                InstructorName = request.InstructorName.Trim(),
                DeliveryModel = request.DeliveryModel.Trim(),
                GradeBand = request.GradeBand.Trim(),
                EnrollmentOpen = request.EnrollmentOpen,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListAcademicAssignmentsUseCase(IAcademicCatalogStore store) : IListAcademicAssignmentsUseCase
{
    public Task<IReadOnlyList<CourseworkAssignment>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListAssignmentsAsync(cancellationToken);
}

public sealed class SaveAcademicAssignmentUseCase(IAcademicCatalogStore store) : ISaveAcademicAssignmentUseCase
{
    public Task<CourseworkAssignment> ExecuteAsync(SaveCourseworkAssignmentRequest request, CancellationToken cancellationToken = default) =>
        store.SaveAssignmentAsync(
            new CourseworkAssignment
            {
                AssignmentId = request.AssignmentId,
                CourseId = request.CourseId,
                CourseTitle = request.CourseTitle.Trim(),
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                DueDate = request.DueDate,
                Audience = request.Audience.Trim(),
                PublishedByUserId = request.PublishedByUserId.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListAcademicSectionsUseCase(IAcademicCatalogStore store) : IListAcademicSectionsUseCase
{
    public Task<IReadOnlyList<CourseSection>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListSectionsAsync(cancellationToken);
}

public sealed class SaveAcademicSectionUseCase(IAcademicCatalogStore store) : ISaveAcademicSectionUseCase
{
    public Task<CourseSection> ExecuteAsync(SaveCourseSectionRequest request, CancellationToken cancellationToken = default) =>
        store.SaveSectionAsync(
            new CourseSection
            {
                SectionId = request.SectionId,
                CourseId = request.CourseId,
                CourseCode = request.CourseCode.Trim(),
                CourseTitle = request.CourseTitle.Trim(),
                SectionCode = request.SectionCode.Trim(),
                TermName = request.TermName.Trim(),
                SchoolYear = request.SchoolYear.Trim(),
                InstructorName = request.InstructorName.Trim(),
                MeetingSchedule = request.MeetingSchedule.Trim(),
                DeliveryModel = request.DeliveryModel.Trim(),
                EnrollmentOpen = request.EnrollmentOpen,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListSectionRosterEntriesUseCase(IAcademicCatalogStore store) : IListSectionRosterEntriesUseCase
{
    public Task<IReadOnlyList<SectionRosterEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        store.ListRosterEntriesAsync(sectionId, cancellationToken);
}

public sealed class SaveSectionRosterEntryUseCase(IAcademicCatalogStore store) : ISaveSectionRosterEntryUseCase
{
    public Task<SectionRosterEntry> ExecuteAsync(SaveSectionRosterEntryRequest request, CancellationToken cancellationToken = default) =>
        store.SaveRosterEntryAsync(
            new SectionRosterEntry
            {
                SectionId = request.SectionId,
                StudentId = request.StudentId,
                StudentCode = request.StudentCode.Trim(),
                StudentName = request.StudentName.Trim(),
                EnrollmentStatus = request.EnrollmentStatus.Trim(),
                EnrolledAtUtc = DateTimeOffset.UtcNow,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListGradebookEntriesUseCase(IAcademicCatalogStore store) : IListGradebookEntriesUseCase
{
    public Task<IReadOnlyList<GradebookEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        store.ListGradebookEntriesAsync(sectionId, cancellationToken);
}

public sealed class SaveGradebookEntryUseCase(IAcademicCatalogStore store) : ISaveGradebookEntryUseCase
{
    public Task<GradebookEntry> ExecuteAsync(SaveGradebookEntryRequest request, CancellationToken cancellationToken = default) =>
        store.SaveGradebookEntryAsync(
            new GradebookEntry
            {
                SectionId = request.SectionId,
                StudentId = request.StudentId,
                AssignmentId = request.AssignmentId,
                AssignmentTitle = request.AssignmentTitle.Trim(),
                ScoreEarned = request.ScoreEarned,
                ScorePossible = request.ScorePossible,
                Status = request.Status.Trim(),
                TeacherComment = string.IsNullOrWhiteSpace(request.TeacherComment) ? null : request.TeacherComment.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListAttendanceEntriesUseCase(IAcademicCatalogStore store) : IListAttendanceEntriesUseCase
{
    public Task<IReadOnlyList<AttendanceEntry>> ExecuteAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        store.ListAttendanceEntriesAsync(sectionId, cancellationToken);
}

public sealed class SaveAttendanceEntryUseCase(IAcademicCatalogStore store) : ISaveAttendanceEntryUseCase
{
    public Task<AttendanceEntry> ExecuteAsync(SaveAttendanceEntryRequest request, CancellationToken cancellationToken = default) =>
        store.SaveAttendanceEntryAsync(
            new AttendanceEntry
            {
                SectionId = request.SectionId,
                StudentId = request.StudentId,
                AttendanceDate = request.AttendanceDate,
                StudentCode = request.StudentCode.Trim(),
                StudentName = request.StudentName.Trim(),
                Status = request.Status.Trim(),
                MinutesPresent = request.MinutesPresent,
                Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
                RecordedByUserId = request.RecordedByUserId.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListAssignmentSubmissionsUseCase(IAcademicCatalogStore store) : IListAssignmentSubmissionsUseCase
{
    public Task<IReadOnlyList<AssignmentSubmission>> ExecuteAsync(Guid sectionId, Guid assignmentId, CancellationToken cancellationToken = default) =>
        store.ListAssignmentSubmissionsAsync(sectionId, assignmentId, cancellationToken);
}

public sealed class SaveAssignmentSubmissionUseCase(IAcademicCatalogStore store) : ISaveAssignmentSubmissionUseCase
{
    public Task<AssignmentSubmission> ExecuteAsync(SaveAssignmentSubmissionRequest request, CancellationToken cancellationToken = default) =>
        store.SaveAssignmentSubmissionAsync(
            new AssignmentSubmission
            {
                SectionId = request.SectionId,
                AssignmentId = request.AssignmentId,
                StudentId = request.StudentId,
                StudentCode = request.StudentCode.Trim(),
                StudentName = request.StudentName.Trim(),
                AssignmentTitle = request.AssignmentTitle.Trim(),
                Status = request.Status.Trim(),
                SubmittedAtUtc = request.SubmittedAtUtc,
                SubmissionType = request.SubmissionType.Trim(),
                ArtifactLabel = request.ArtifactLabel.Trim(),
                Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
                ReviewedAtUtc = request.ReviewedAtUtc,
                ReviewedByUserId = string.IsNullOrWhiteSpace(request.ReviewedByUserId) ? null : request.ReviewedByUserId.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListStudentAcademicRecordsUseCase(IAcademicCatalogStore store) : IListStudentAcademicRecordsUseCase
{
    public Task<IReadOnlyList<StudentAcademicRecord>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListRecordsAsync(cancellationToken);
}

public sealed class GetStudentAcademicRecordByStudentIdUseCase(IAcademicCatalogStore store) : IGetStudentAcademicRecordByStudentIdUseCase
{
    public Task<StudentAcademicRecord?> ExecuteAsync(Guid studentId, CancellationToken cancellationToken = default) =>
        store.GetRecordByStudentIdAsync(studentId, cancellationToken);
}

public sealed class GetStudentTranscriptSummaryByStudentIdUseCase(IAcademicCatalogStore store) : IGetStudentTranscriptSummaryByStudentIdUseCase
{
    public async Task<StudentTranscriptSummary?> ExecuteAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        var record = await store.GetRecordByStudentIdAsync(studentId, cancellationToken);
        if (record is null)
        {
            return null;
        }

        var termSummaries = record.TranscriptTerms
            .OrderBy(term => term.SchoolYear, StringComparer.OrdinalIgnoreCase)
            .ThenBy(term => term.TermName, StringComparer.OrdinalIgnoreCase)
            .Select(term => new StudentTranscriptTermSummary
            {
                SchoolYear = term.SchoolYear,
                TermName = term.TermName,
                TermGpa = term.TermGpa,
                CreditsEarned = term.Courses.Sum(course => course.CreditsEarned ?? 0m),
                CourseCount = term.Courses.Count,
            })
            .ToList();

        var totalCredits = record.Courses.Sum(course => course.CreditsEarned ?? 0m);
        var completedCourseCount = record.Courses.Count(course =>
            string.Equals(course.Status, "Completed", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(course.Status, "Passed", StringComparison.OrdinalIgnoreCase));

        return new StudentTranscriptSummary
        {
            StudentId = record.StudentId,
            StudentCode = record.StudentCode,
            StudentName = record.StudentName,
            GradeLevel = record.GradeLevel,
            CumulativeGpa = record.CumulativeGpa,
            TotalCreditsEarned = totalCredits,
            CompletedCourseCount = completedCourseCount,
            AcademicStanding = record.CumulativeGpa switch
            {
                >= 3.8m => "High Honors",
                >= 3.5m => "Honors",
                >= 2.0m => "Good Standing",
                not null => "Academic Support",
                _ => "In Progress",
            },
            Terms = termSummaries,
            UpdatedAtUtc = record.UpdatedAtUtc,
        };
    }
}

public sealed class SaveStudentAcademicRecordUseCase(IAcademicCatalogStore store) : ISaveStudentAcademicRecordUseCase
{
    public Task<StudentAcademicRecord> ExecuteAsync(SaveStudentAcademicRecordRequest request, CancellationToken cancellationToken = default) =>
        store.SaveRecordAsync(
            new StudentAcademicRecord
            {
                StudentId = request.StudentId,
                StudentCode = request.StudentCode.Trim(),
                StudentName = request.StudentName.Trim(),
                GradeLevel = request.GradeLevel.Trim(),
                Courses = request.Courses.ToList(),
                TranscriptTerms = request.TranscriptTerms.ToList(),
                CumulativeGpa = request.CumulativeGpa,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}
