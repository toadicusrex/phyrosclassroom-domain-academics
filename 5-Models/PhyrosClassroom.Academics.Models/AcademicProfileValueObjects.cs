namespace PhyrosClassroom.Academics.Models;

public sealed record AcademicContact(
    string FullName,
    string RelationshipToChildren,
    string Email,
    string Phone,
    bool IsPrimaryContact,
    bool WantsPortalAccess);

public sealed record AcademicStudent(
    Guid? StudentId,
    string StudentCode,
    string GivenName,
    string FamilyName,
    string GradeLevel,
    DateOnly? BirthDate,
    string RelationshipToPrimaryContact);

public sealed record AcademicDocumentReference(
    string DocumentType,
    string FileName,
    DateTimeOffset UploadedAtUtc,
    string UploadedByUserId);

public sealed record AcademicRecordNote(
    string Category,
    string Body,
    DateTimeOffset RecordedAtUtc,
    string RecordedByUserId);
