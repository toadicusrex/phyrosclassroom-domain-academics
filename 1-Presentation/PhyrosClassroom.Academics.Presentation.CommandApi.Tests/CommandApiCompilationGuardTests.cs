namespace PhyrosClassroom.Academics.Presentation.CommandApi.Tests;

public sealed class CommandApiCompilationGuardTests
{
    [Fact]
    public void PresentationAssembly_IsLoadable()
    {
        var assembly = typeof(PhyrosClassroom.Academics.Presentation.CommandApi.CommandApiEndpointRouteBuilderExtensions).Assembly;

        Assert.NotNull(assembly);
    }
}
