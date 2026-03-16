using PhyrosClassroom.Academics.Engines;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class RegisterAcademicUseCase(
    IAcademicCodeGenerator codeGenerator,
    IAcademicEventStore eventStore,
    IAcademicReadModelStore readModelStore,
    IAcademicHydratedModelCache hydratedModelCache) : IRegisterAcademicUseCase
{
    private const string IdentitySubjectSourceSystem = "identity-subject";

    public async Task<AcademicAggregate> ExecuteAsync(RegisterAcademicRequest request, CancellationToken cancellationToken = default)
    {
        var sourceSystem = string.IsNullOrWhiteSpace(request.SourceSystem) ? IdentitySubjectSourceSystem : request.SourceSystem.Trim();
        var sourceReference = string.IsNullOrWhiteSpace(request.SourceReference) ? request.SubjectId.Trim() : request.SourceReference.Trim();

        if (sourceSystem is not null && sourceReference is not null)
        {
            var existingReadModel = await readModelStore.GetBySourceReferenceAsync(sourceSystem, sourceReference, cancellationToken);
            if (existingReadModel is not null)
            {
                var cachedAcademic = await hydratedModelCache.GetAsync(existingReadModel.AcademicId, cancellationToken);
                if (cachedAcademic is not null)
                {
                    return cachedAcademic;
                }

                var eventHistory = await eventStore.GetByIdAsync(existingReadModel.AcademicId, cancellationToken);
                var hydratedAcademic = AcademicAggregate.Rehydrate(eventHistory);
                if (hydratedAcademic is not null)
                {
                    await hydratedModelCache.SetAsync(hydratedAcademic, cancellationToken);
                    return hydratedAcademic;
                }
            }
        }

        var academicId = Guid.NewGuid();
        var occurredUtc = DateTimeOffset.UtcNow;
        var academicCode = codeGenerator.GenerateCode(request.GivenName, request.FamilyName, occurredUtc);

        var academic = AcademicAggregate.Register(
            academicId,
            academicCode,
            request.GivenName,
            request.FamilyName,
            occurredUtc,
            request.SubjectId,
            request.AcademicName,
            request.WantsEmailNotifications,
            request.WantsSmsNotifications,
            request.OnboardingStatus,
            request.Contacts,
            request.Students,
            request.SourceRegistrationId,
            request.SourceRegistrationStatus,
            request.SourceRegistrationUpdatedAtUtc,
            sourceSystem,
            sourceReference);

        await eventStore.AppendAsync(academic.AcademicId, academic.Events, cancellationToken);
        await readModelStore.UpsertAsync(academic.ToReadModel(), cancellationToken);
        await hydratedModelCache.SetAsync(academic, cancellationToken);

        return academic;
    }
}
