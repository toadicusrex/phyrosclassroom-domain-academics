using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public sealed record SaveCourseCatalogEntryRequest(
    Guid CourseId,
    string CourseCode,
    string Title,
    string Description,
    string Department,
    string InstructorName,
    string DeliveryModel,
    string GradeBand,
    bool EnrollmentOpen);

public sealed record SaveCourseworkAssignmentRequest(
    Guid AssignmentId,
    Guid CourseId,
    string CourseTitle,
    string Title,
    string Description,
    DateOnly DueDate,
    string Audience,
    string PublishedByUserId);

public sealed record SaveCourseSectionRequest(
    Guid SectionId,
    Guid CourseId,
    string CourseCode,
    string CourseTitle,
    string SectionCode,
    string TermName,
    string SchoolYear,
    string InstructorName,
    string MeetingSchedule,
    string DeliveryModel,
    bool EnrollmentOpen);

public sealed record SaveSectionRosterEntryRequest(
    Guid SectionId,
    Guid StudentId,
    string StudentCode,
    string StudentName,
    string EnrollmentStatus);

public sealed record SaveGradebookEntryRequest(
    Guid SectionId,
    Guid StudentId,
    Guid AssignmentId,
    string AssignmentTitle,
    decimal? ScoreEarned,
    decimal? ScorePossible,
    string Status,
    string? TeacherComment);

public sealed record SaveStudentAcademicRecordRequest(
    Guid StudentId,
    string StudentCode,
    string StudentName,
    string GradeLevel,
    IReadOnlyList<StudentCourseRecord> Courses,
    IReadOnlyList<StudentTranscriptTerm> TranscriptTerms,
    decimal? CumulativeGpa);
