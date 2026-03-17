namespace PhyrosClassroom.Academics.Models;

public sealed class SectionOperationsSummary
{
    public Guid SectionId { get; set; }
    public string SectionCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
    public int ActiveRosterCount { get; set; }
    public int MissingSubmissionCount { get; set; }
    public int AttendanceRiskCount { get; set; }
    public int FailingStudentCount { get; set; }
    public decimal? AverageScorePercent { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}
