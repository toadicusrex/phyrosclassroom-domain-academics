using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class GetAcademicByIdUseCase(
    IAcademicEventStore eventStore,
    IAcademicHydratedModelCache hydratedModelCache) : IGetAcademicByIdUseCase
{
    public async Task<AcademicAggregate?> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        var cachedAcademic = await hydratedModelCache.GetAsync(academicId, cancellationToken);
        if (cachedAcademic is not null)
        {
            return cachedAcademic;
        }

        var eventHistory = await eventStore.GetByIdAsync(academicId, cancellationToken);
        var academic = AcademicAggregate.Rehydrate(eventHistory);

        if (academic is not null)
        {
            await hydratedModelCache.SetAsync(academic, cancellationToken);
        }

        return academic;
    }
}
