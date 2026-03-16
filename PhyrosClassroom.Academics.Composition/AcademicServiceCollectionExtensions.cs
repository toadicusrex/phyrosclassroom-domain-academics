using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhyrosClassroom.Academics.Engines;
using PhyrosClassroom.Academics.Engines.Default;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Infrastructure.Persistence.Default;
using PhyrosClassroom.Academics.Orchestration;
using PhyrosClassroom.Academics.Orchestration.Default;

namespace PhyrosClassroom.Academics.Composition;

public static class AcademicServiceCollectionExtensions
{
    public static IServiceCollection AddAcademicCommandServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAcademicSharedServices(configuration);
        services.AddScoped<IRegisterAcademicUseCase, RegisterAcademicUseCase>();
        services.AddScoped<IUpdateAcademicProfileUseCase, UpdateAcademicProfileUseCase>();
        services.AddScoped<IGetAcademicByIdUseCase, GetAcademicByIdUseCase>();
        services.AddScoped<IGetAcademicAtPointInTimeUseCase, GetAcademicAtPointInTimeUseCase>();
        services.AddScoped<IGetAcademicEventHistoryUseCase, GetAcademicEventHistoryUseCase>();

        return services;
    }

    public static IServiceCollection AddAcademicQueryServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAcademicSharedServices(configuration);
        services.AddScoped<IListAcademicsUseCase, ListAcademicsUseCase>();
        services.AddScoped<IGetAcademicReadModelByIdUseCase, GetAcademicReadModelByIdUseCase>();
        services.AddScoped<IGetAcademicReadModelBySubjectIdUseCase, GetAcademicReadModelBySubjectIdUseCase>();

        return services;
    }

    private static IServiceCollection AddAcademicSharedServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<AcademicStorageOptions>()
            .Bind(configuration.GetSection(AcademicStorageOptions.SectionName));

        services.AddSingleton<IAcademicEventStore, FileAcademicEventStore>();
        services.AddSingleton<IAcademicReadModelStore, FileAcademicReadModelStore>();
        services.AddSingleton<IAcademicHydratedModelCache, InMemoryAcademicHydratedModelCache>();
        services.AddSingleton<IAcademicCodeGenerator, AcademicCodeGenerator>();

        return services;
    }
}
