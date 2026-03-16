namespace PhyrosClassroom.Academics.Presentation.QueryApi.Tests;

public sealed class QueryApiCompilationGuardTests
{
    [Fact]
    public void PresentationAssembly_IsLoadable()
    {
        var assembly = typeof(PhyrosClassroom.Academics.Presentation.QueryApi.QueryApiEndpointRouteBuilderExtensions).Assembly;

        Assert.NotNull(assembly);
    }
}
