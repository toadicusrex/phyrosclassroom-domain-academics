using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Orchestration;

public interface IListAcademicsUseCase
{
    Task<IReadOnlyList<AcademicReadModel>> ExecuteAsync(CancellationToken cancellationToken = default);
}
