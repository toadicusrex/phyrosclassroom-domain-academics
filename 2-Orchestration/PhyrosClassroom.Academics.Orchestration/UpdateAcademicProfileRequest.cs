using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public sealed record UpdateAcademicProfileRequest(
    Guid AcademicId,
    string AcademicName,
    bool WantsEmailNotifications,
    bool WantsSmsNotifications,
    string? OnboardingStatus,
    IReadOnlyList<AcademicContact> Contacts,
    IReadOnlyList<AcademicStudent> Students,
    Guid? SourceRegistrationId,
    string? SourceRegistrationStatus,
    DateTimeOffset? SourceRegistrationUpdatedAtUtc,
    DateOnly? BirthDate,
    string? GradeLevel,
    string? PrimaryGuardianName,
    string? PrimaryGuardianEmail,
    bool HasMedicalAlert,
    string? MedicalNotes,
    bool HasIep,
    IReadOnlyList<AcademicDocumentReference> Documents,
    IReadOnlyList<AcademicRecordNote> Notes);
