namespace PhyrosClassroom.Academics.Models;

public sealed class CourseCatalogEntry
{
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
    public string DeliveryModel { get; set; } = string.Empty;
    public string GradeBand { get; set; } = string.Empty;
    public bool EnrollmentOpen { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class CourseworkAssignment
{
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public string Audience { get; set; } = "All";
    public string PublishedByUserId { get; set; } = string.Empty;
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class CourseSection
{
    public Guid SectionId { get; set; }
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string SectionCode { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public string SchoolYear { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
    public string MeetingSchedule { get; set; } = string.Empty;
    public string DeliveryModel { get; set; } = string.Empty;
    public bool EnrollmentOpen { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class SectionRosterEntry
{
    public Guid SectionId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string EnrollmentStatus { get; set; } = string.Empty;
    public DateTimeOffset EnrolledAtUtc { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class GradebookEntry
{
    public Guid SectionId { get; set; }
    public Guid StudentId { get; set; }
    public Guid AssignmentId { get; set; }
    public string AssignmentTitle { get; set; } = string.Empty;
    public decimal? ScoreEarned { get; set; }
    public decimal? ScorePossible { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? TeacherComment { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class AttendanceEntry
{
    public Guid SectionId { get; set; }
    public Guid StudentId { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? MinutesPresent { get; set; }
    public string? Notes { get; set; }
    public string RecordedByUserId { get; set; } = string.Empty;
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class AssignmentSubmission
{
    public Guid SectionId { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string AssignmentTitle { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset SubmittedAtUtc { get; set; }
    public string SubmissionType { get; set; } = string.Empty;
    public string ArtifactLabel { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTimeOffset? ReviewedAtUtc { get; set; }
    public string? ReviewedByUserId { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class StudentAcademicRecord
{
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public List<StudentCourseRecord> Courses { get; set; } = [];
    public List<StudentTranscriptTerm> TranscriptTerms { get; set; } = [];
    public decimal? CumulativeGpa { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}

public sealed class StudentCourseRecord
{
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string FinalGrade { get; set; } = string.Empty;
    public decimal? CreditsEarned { get; set; }
    public string Status { get; set; } = string.Empty;
}

public sealed class StudentTranscriptTerm
{
    public string SchoolYear { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public decimal? TermGpa { get; set; }
    public List<StudentCourseRecord> Courses { get; set; } = [];
}
