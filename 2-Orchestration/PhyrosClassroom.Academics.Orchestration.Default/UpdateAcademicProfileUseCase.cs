using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class UpdateAcademicProfileUseCase(
    IAcademicEventStore eventStore,
    IAcademicReadModelStore readModelStore,
    IAcademicHydratedModelCache hydratedModelCache) : IUpdateAcademicProfileUseCase
{
    public async Task<AcademicAggregate> ExecuteAsync(UpdateAcademicProfileRequest request, CancellationToken cancellationToken = default)
    {
        var academic = await hydratedModelCache.GetAsync(request.AcademicId, cancellationToken);
        if (academic is null)
        {
            var eventHistory = await eventStore.GetByIdAsync(request.AcademicId, cancellationToken);
            academic = AcademicAggregate.Rehydrate(eventHistory);
        }

        if (academic is null)
        {
            throw new InvalidOperationException("Academic was not found.");
        }

        academic.UpdateProfile(
            request.AcademicName,
            request.WantsEmailNotifications,
            request.WantsSmsNotifications,
            request.OnboardingStatus,
            request.Contacts ?? [],
            request.Students ?? [],
            request.SourceRegistrationId,
            request.SourceRegistrationStatus,
            request.SourceRegistrationUpdatedAtUtc,
            request.BirthDate,
            request.GradeLevel,
            request.PrimaryGuardianName,
            request.PrimaryGuardianEmail,
            request.HasMedicalAlert,
            request.MedicalNotes,
            request.HasIep,
            request.Documents ?? [],
            request.Notes ?? [],
            DateTimeOffset.UtcNow);

        await eventStore.AppendAsync(academic.AcademicId, [academic.Events[^1]], cancellationToken);
        await readModelStore.UpsertAsync(academic.ToReadModel(), cancellationToken);
        await hydratedModelCache.SetAsync(academic, cancellationToken);

        return academic;
    }
}
