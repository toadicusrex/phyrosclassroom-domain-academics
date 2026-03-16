using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhyrosClassroom.Academics.Composition;
using PhyrosClassroom.Academics.Models;
using PhyrosClassroom.Academics.Orchestration;
using Reqnroll;

namespace PhyrosClassroom.Academics.ApplicationCore;

[Binding]
public sealed class AcademicsRegistrationSteps
{
    private ServiceProvider? _serviceProvider;
    private AcademicAggregate? _registeredAcademic;
    private AcademicReadModel? _queriedAcademic;

    [Given("the Academics application composition is configured")]
    public void GivenTheAcademicsApplicationCompositionIsConfigured()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["AcademicStorage:BasePath"] = Path.Combine(
                    Path.GetTempPath(),
                    "phyrosclassroom-academics-tests",
                    Guid.NewGuid().ToString("N")),
            })
            .Build();

        var services = new ServiceCollection();
        services.AddAcademicCommandServices(configuration);
        services.AddAcademicQueryServices(configuration);

        _serviceProvider = services.BuildServiceProvider();
    }

    [When("I register a academic named {string} {string}")]
    public async Task WhenIRegisterAAcademicNamed(string givenName, string familyName)
    {
        var useCase = _serviceProvider!.GetRequiredService<IRegisterAcademicUseCase>();
        _registeredAcademic = await useCase.ExecuteAsync(new RegisterAcademicRequest(givenName, familyName));
    }

    [Then("the registered academic can be retrieved from the query side")]
    public async Task ThenTheRegisteredAcademicCanBeRetrievedFromTheQuerySide()
    {
        var useCase = _serviceProvider!.GetRequiredService<IGetAcademicReadModelByIdUseCase>();
        _queriedAcademic = await useCase.ExecuteAsync(_registeredAcademic!.AcademicId);

        Assert.NotNull(_queriedAcademic);
        Assert.Equal(_registeredAcademic.AcademicId, _queriedAcademic!.AcademicId);
        Assert.Equal(_registeredAcademic.GivenName, _queriedAcademic.GivenName);
        Assert.Equal(_registeredAcademic.FamilyName, _queriedAcademic.FamilyName);
    }
}
