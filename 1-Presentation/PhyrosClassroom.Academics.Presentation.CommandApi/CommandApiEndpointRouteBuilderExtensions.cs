using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PhyrosClassroom.Academics.Orchestration;

namespace PhyrosClassroom.Academics.Presentation.CommandApi;

public static class CommandApiEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAcademicCommandApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/command/academics");

        group.MapPost("/", async (
            RegisterAcademicRequest request,
            IRegisterAcademicUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(request, cancellationToken);
            return Results.Created($"/command/academics/{academic.AcademicId}", academic);
        });

        group.MapGet("/{academicId:guid}", async (
            Guid academicId,
            IGetAcademicByIdUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(academicId, cancellationToken);
            return academic is null ? Results.NotFound() : Results.Ok(academic);
        });

        group.MapGet("/{academicId:guid}/point-in-time", async (
            Guid academicId,
            DateTimeOffset at,
            IGetAcademicAtPointInTimeUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(academicId, at, cancellationToken);
            return academic is null ? Results.NotFound() : Results.Ok(academic);
        });

        group.MapGet("/{academicId:guid}/history", async (
            Guid academicId,
            IGetAcademicEventHistoryUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var history = await useCase.ExecuteAsync(academicId, cancellationToken);
            return Results.Ok(history);
        });

        group.MapPut("/{academicId:guid}/profile", async (
            Guid academicId,
            UpdateAcademicProfileInput input,
            IUpdateAcademicProfileUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(
                new UpdateAcademicProfileRequest(
                    academicId,
                    input.AcademicName,
                    input.WantsEmailNotifications,
                    input.WantsSmsNotifications,
                    input.OnboardingStatus,
                    input.Contacts.Select(contact => new Academics.Models.AcademicContact(
                        contact.FullName,
                        contact.RelationshipToChildren,
                        contact.Email,
                        contact.Phone,
                        contact.IsPrimaryContact,
                        contact.WantsPortalAccess)).ToArray(),
                    input.Students.Select(student => new Academics.Models.AcademicStudent(
                        student.StudentId,
                        student.StudentCode,
                        student.GivenName,
                        student.FamilyName,
                        student.GradeLevel,
                        student.BirthDate,
                        student.RelationshipToPrimaryContact)).ToArray(),
                    input.SourceRegistrationId,
                    input.SourceRegistrationStatus,
                    input.SourceRegistrationUpdatedAtUtc,
                    input.BirthDate,
                    input.GradeLevel,
                    input.PrimaryGuardianName,
                    input.PrimaryGuardianEmail,
                    input.HasMedicalAlert,
                    input.MedicalNotes,
                    input.HasIep,
                    input.Documents.Select(document => new Academics.Models.AcademicDocumentReference(
                        document.DocumentType,
                        document.FileName,
                        document.UploadedAtUtc,
                        document.UploadedByUserId)).ToArray(),
                    input.Notes.Select(note => new Academics.Models.AcademicRecordNote(
                        note.Category,
                        note.Body,
                        note.RecordedAtUtc,
                        note.RecordedByUserId)).ToArray()),
                cancellationToken);

            return Results.Ok(academic);
        });

        return endpoints;
    }
}

public sealed class UpdateAcademicProfileInput
{
    public string AcademicName { get; set; } = string.Empty;
    public bool WantsEmailNotifications { get; set; }
    public bool WantsSmsNotifications { get; set; }
    public string? OnboardingStatus { get; set; }
    public Guid? SourceRegistrationId { get; set; }
    public string? SourceRegistrationStatus { get; set; }
    public DateTimeOffset? SourceRegistrationUpdatedAtUtc { get; set; }
    public List<AcademicContactInput> Contacts { get; set; } = [];
    public List<AcademicStudentInput> Students { get; set; } = [];
    public DateOnly? BirthDate { get; set; }
    public string? GradeLevel { get; set; }
    public string? PrimaryGuardianName { get; set; }
    public string? PrimaryGuardianEmail { get; set; }
    public bool HasMedicalAlert { get; set; }
    public string? MedicalNotes { get; set; }
    public bool HasIep { get; set; }
    public List<AcademicDocumentInput> Documents { get; set; } = [];
    public List<AcademicNoteInput> Notes { get; set; } = [];
}

public sealed class AcademicContactInput
{
    public string FullName { get; set; } = string.Empty;
    public string RelationshipToChildren { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsPrimaryContact { get; set; }
    public bool WantsPortalAccess { get; set; }
}

public sealed class AcademicStudentInput
{
    public Guid? StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public string RelationshipToPrimaryContact { get; set; } = string.Empty;
}

public sealed class AcademicDocumentInput
{
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTimeOffset UploadedAtUtc { get; set; }
    public string UploadedByUserId { get; set; } = string.Empty;
}

public sealed class AcademicNoteInput
{
    public string Category { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTimeOffset RecordedAtUtc { get; set; }
    public string RecordedByUserId { get; set; } = string.Empty;
}
