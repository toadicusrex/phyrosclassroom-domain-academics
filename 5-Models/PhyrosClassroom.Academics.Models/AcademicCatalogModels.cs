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
