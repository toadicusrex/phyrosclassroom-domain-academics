using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class GetAcademicReadModelBySubjectIdUseCase(IAcademicReadModelStore readModelStore) : IGetAcademicReadModelBySubjectIdUseCase
{
    private const string SourceSystem = "identity-subject";

    public Task<AcademicReadModel?> ExecuteAsync(string subjectId, CancellationToken cancellationToken = default)
    {
        return readModelStore.GetBySourceReferenceAsync(SourceSystem, subjectId.Trim(), cancellationToken);
    }
}
