using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class ListAcademicsUseCase(IAcademicReadModelStore readModelStore) : IListAcademicsUseCase
{
    public Task<IReadOnlyList<AcademicReadModel>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return readModelStore.ListAsync(cancellationToken);
    }
}
