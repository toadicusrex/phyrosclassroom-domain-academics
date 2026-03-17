namespace PhyrosClassroom.Academics.Models;

public sealed class AcademicOperationalAlert
{
    public Guid SectionId { get; set; }
    public string SectionCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateOnly? RelevantDate { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}
