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

public sealed class ListAcademicOperationalAlertsUseCase(IAcademicCatalogStore store) : IListAcademicOperationalAlertsUseCase
{
    public async Task<IReadOnlyList<AcademicOperationalAlert>> ExecuteAsync(DateOnly? asOfDate = null, CancellationToken cancellationToken = default)
    {
        var effectiveDate = asOfDate ?? DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var sections = await store.ListSectionsAsync(cancellationToken);
        var assignments = await store.ListAssignmentsAsync(cancellationToken);
        var alerts = new List<AcademicOperationalAlert>();

        foreach (var section in sections)
        {
            var roster = await store.ListRosterEntriesAsync(section.SectionId, cancellationToken);
            var gradebook = await store.ListGradebookEntriesAsync(section.SectionId, cancellationToken);
            var attendance = await store.ListAttendanceEntriesAsync(section.SectionId, cancellationToken);
            var sectionAssignments = assignments.Where(assignment => assignment.CourseId == section.CourseId).ToArray();

            foreach (var student in roster.Where(entry => !string.Equals(entry.EnrollmentStatus, "Dropped", StringComparison.OrdinalIgnoreCase)))
            {
                var studentGrades = gradebook.Where(entry => entry.StudentId == student.StudentId).ToArray();
                var failingGrades = studentGrades
                    .Where(entry =>
                        entry.ScoreEarned.HasValue &&
                        entry.ScorePossible.HasValue &&
                        entry.ScorePossible.Value > 0m &&
                        entry.ScoreEarned.Value / entry.ScorePossible.Value < 0.70m)
                    .ToArray();

                if (failingGrades.Length > 0)
                {
                    var lowestGrade = failingGrades
                        .Select(entry => Math.Round(entry.ScoreEarned!.Value / entry.ScorePossible!.Value * 100m, 0))
                        .DefaultIfEmpty(0m)
                        .Min();

                    alerts.Add(new AcademicOperationalAlert
                    {
                        SectionId = section.SectionId,
                        SectionCode = section.SectionCode,
                        CourseTitle = section.CourseTitle,
                        StudentId = student.StudentId,
                        StudentCode = student.StudentCode,
                        StudentName = student.StudentName,
                        AlertType = "FailingGrade",
                        Severity = "High",
                        Message = $"Student has {failingGrades.Length} low gradebook entries; lowest score is {lowestGrade:0}%.",
                        UpdatedAtUtc = failingGrades.Max(entry => entry.UpdatedAtUtc),
                    });
                }

                var studentAttendance = attendance.Where(entry => entry.StudentId == student.StudentId).ToArray();
                var absences = studentAttendance.Count(entry =>
                    string.Equals(entry.Status, "Absent", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(entry.Status, "Unexcused", StringComparison.OrdinalIgnoreCase));

                if (absences >= 3)
                {
                    alerts.Add(new AcademicOperationalAlert
                    {
                        SectionId = section.SectionId,
                        SectionCode = section.SectionCode,
                        CourseTitle = section.CourseTitle,
                        StudentId = student.StudentId,
                        StudentCode = student.StudentCode,
                        StudentName = student.StudentName,
                        AlertType = "AttendanceRisk",
                        Severity = "Medium",
                        Message = $"Student has {absences} absence records in this section.",
                        RelevantDate = studentAttendance
                            .Where(entry => string.Equals(entry.Status, "Absent", StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(entry.Status, "Unexcused", StringComparison.OrdinalIgnoreCase))
                            .Select(entry => entry.AttendanceDate)
                            .DefaultIfEmpty(effectiveDate)
                            .Max(),
                        UpdatedAtUtc = studentAttendance.Max(entry => entry.UpdatedAtUtc),
                    });
                }

                foreach (var assignment in sectionAssignments.Where(assignment => assignment.DueDate < effectiveDate))
                {
                    var submission = await store.ListAssignmentSubmissionsAsync(section.SectionId, assignment.AssignmentId, cancellationToken);
                    var studentSubmission = submission.FirstOrDefault(entry => entry.StudentId == student.StudentId);
                    if (studentSubmission is null ||
                        string.Equals(studentSubmission.Status, "Missing", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(studentSubmission.Status, "NotSubmitted", StringComparison.OrdinalIgnoreCase))
                    {
                        alerts.Add(new AcademicOperationalAlert
                        {
                            SectionId = section.SectionId,
                            SectionCode = section.SectionCode,
                            CourseTitle = section.CourseTitle,
                            StudentId = student.StudentId,
                            StudentCode = student.StudentCode,
                            StudentName = student.StudentName,
                            AlertType = "MissingSubmission",
                            Severity = "Medium",
                            Message = $"Missing submission for \"{assignment.Title}\" due {assignment.DueDate:yyyy-MM-dd}.",
                            RelevantDate = assignment.DueDate,
                            UpdatedAtUtc = studentSubmission?.UpdatedAtUtc ?? assignment.UpdatedAtUtc,
                        });
                    }
                }
            }
        }

        return alerts
            .OrderByDescending(alert => alert.Severity, StringComparer.OrdinalIgnoreCase)
            .ThenBy(alert => alert.RelevantDate ?? DateOnly.MaxValue)
            .ThenBy(alert => alert.StudentName, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}

public sealed class ListSectionOperationsSummariesUseCase(IAcademicCatalogStore store) : IListSectionOperationsSummariesUseCase
{
    public async Task<IReadOnlyList<SectionOperationsSummary>> ExecuteAsync(DateOnly? asOfDate = null, CancellationToken cancellationToken = default)
    {
        var effectiveDate = asOfDate ?? DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var sections = await store.ListSectionsAsync(cancellationToken);
        var assignments = await store.ListAssignmentsAsync(cancellationToken);
        var summaries = new List<SectionOperationsSummary>(sections.Count);

        foreach (var section in sections)
        {
            var roster = (await store.ListRosterEntriesAsync(section.SectionId, cancellationToken))
                .Where(entry => !string.Equals(entry.EnrollmentStatus, "Dropped", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            var gradebook = await store.ListGradebookEntriesAsync(section.SectionId, cancellationToken);
            var attendance = await store.ListAttendanceEntriesAsync(section.SectionId, cancellationToken);
            var sectionAssignments = assignments.Where(assignment => assignment.CourseId == section.CourseId).ToArray();

            var failingStudentCount = roster.Count(student => gradebook.Any(entry =>
                entry.StudentId == student.StudentId &&
                entry.ScoreEarned.HasValue &&
                entry.ScorePossible.HasValue &&
                entry.ScorePossible.Value > 0m &&
                entry.ScoreEarned.Value / entry.ScorePossible.Value < 0.70m));

            var attendanceRiskCount = roster.Count(student =>
                attendance.Count(entry =>
                    entry.StudentId == student.StudentId &&
                    (string.Equals(entry.Status, "Absent", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(entry.Status, "Unexcused", StringComparison.OrdinalIgnoreCase))) >= 3);

            var missingSubmissionCount = 0;
            foreach (var assignment in sectionAssignments.Where(assignment => assignment.DueDate < effectiveDate))
            {
                var submissions = await store.ListAssignmentSubmissionsAsync(section.SectionId, assignment.AssignmentId, cancellationToken);
                missingSubmissionCount += roster.Count(student =>
                {
                    var submission = submissions.FirstOrDefault(entry => entry.StudentId == student.StudentId);
                    return submission is null ||
                           string.Equals(submission.Status, "Missing", StringComparison.OrdinalIgnoreCase) ||
                           string.Equals(submission.Status, "NotSubmitted", StringComparison.OrdinalIgnoreCase);
                });
            }

            var scoredEntries = gradebook
                .Where(entry => entry.ScoreEarned.HasValue && entry.ScorePossible.HasValue && entry.ScorePossible.Value > 0m)
                .ToArray();
            decimal? averageScore = scoredEntries.Length == 0
                ? null
                : Math.Round(scoredEntries.Average(entry => entry.ScoreEarned!.Value / entry.ScorePossible!.Value * 100m), 1);

            summaries.Add(new SectionOperationsSummary
            {
                SectionId = section.SectionId,
                SectionCode = section.SectionCode,
                CourseTitle = section.CourseTitle,
                TermName = section.TermName,
                InstructorName = section.InstructorName,
                ActiveRosterCount = roster.Length,
                MissingSubmissionCount = missingSubmissionCount,
                AttendanceRiskCount = attendanceRiskCount,
                FailingStudentCount = failingStudentCount,
                AverageScorePercent = averageScore,
                UpdatedAtUtc = new[]
                    {
                        section.UpdatedAtUtc,
                        roster.Select(entry => entry.UpdatedAtUtc).DefaultIfEmpty(section.UpdatedAtUtc).Max(),
                        gradebook.Select(entry => entry.UpdatedAtUtc).DefaultIfEmpty(section.UpdatedAtUtc).Max(),
                        attendance.Select(entry => entry.UpdatedAtUtc).DefaultIfEmpty(section.UpdatedAtUtc).Max(),
                    }
                    .Max(),
            });
        }

        return summaries
            .OrderByDescending(summary => summary.MissingSubmissionCount + summary.AttendanceRiskCount + summary.FailingStudentCount)
            .ThenBy(summary => summary.SectionCode, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}
