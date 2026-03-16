using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class GetAcademicReadModelByIdUseCase(IAcademicReadModelStore readModelStore) : IGetAcademicReadModelByIdUseCase
{
    public Task<AcademicReadModel?> ExecuteAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        return readModelStore.GetByIdAsync(academicId, cancellationToken);
    }
}
