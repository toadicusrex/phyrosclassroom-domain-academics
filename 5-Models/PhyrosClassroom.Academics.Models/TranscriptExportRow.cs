namespace PhyrosClassroom.Academics.Models;

public sealed class TranscriptExportRow
{
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public string SchoolYear { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public string CourseCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string FinalGrade { get; set; } = string.Empty;
    public decimal? CreditsEarned { get; set; }
    public decimal? TermGpa { get; set; }
    public decimal? CumulativeGpa { get; set; }
    public string Status { get; set; } = string.Empty;
}
