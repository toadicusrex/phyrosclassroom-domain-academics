using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public sealed record RegisterAcademicRequest(
    string GivenName,
    string FamilyName,
    string SubjectId = "",
    string AcademicName = "",
    bool WantsEmailNotifications = false,
    bool WantsSmsNotifications = false,
    string? OnboardingStatus = null,
    IReadOnlyList<AcademicContact>? Contacts = null,
    IReadOnlyList<AcademicStudent>? Students = null,
    Guid? SourceRegistrationId = null,
    string? SourceRegistrationStatus = null,
    DateTimeOffset? SourceRegistrationUpdatedAtUtc = null,
    string? SourceSystem = null,
    string? SourceReference = null);
