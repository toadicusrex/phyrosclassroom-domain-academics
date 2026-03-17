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
        services.AddScoped<ISaveAcademicCourseUseCase, SaveAcademicCourseUseCase>();
        services.AddScoped<ISaveAcademicAssignmentUseCase, SaveAcademicAssignmentUseCase>();
        services.AddScoped<ISaveAcademicSectionUseCase, SaveAcademicSectionUseCase>();
        services.AddScoped<ISaveSectionRosterEntryUseCase, SaveSectionRosterEntryUseCase>();
        services.AddScoped<ISaveGradebookEntryUseCase, SaveGradebookEntryUseCase>();
        services.AddScoped<ISaveAttendanceEntryUseCase, SaveAttendanceEntryUseCase>();
        services.AddScoped<ISaveAssignmentSubmissionUseCase, SaveAssignmentSubmissionUseCase>();
        services.AddScoped<ISaveStudentAcademicRecordUseCase, SaveStudentAcademicRecordUseCase>();

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
        services.AddScoped<IListAcademicCoursesUseCase, ListAcademicCoursesUseCase>();
        services.AddScoped<IListAcademicAssignmentsUseCase, ListAcademicAssignmentsUseCase>();
        services.AddScoped<IListAcademicSectionsUseCase, ListAcademicSectionsUseCase>();
        services.AddScoped<IListSectionRosterEntriesUseCase, ListSectionRosterEntriesUseCase>();
        services.AddScoped<IListGradebookEntriesUseCase, ListGradebookEntriesUseCase>();
        services.AddScoped<IListAttendanceEntriesUseCase, ListAttendanceEntriesUseCase>();
        services.AddScoped<IListAssignmentSubmissionsUseCase, ListAssignmentSubmissionsUseCase>();
        services.AddScoped<IListStudentAcademicRecordsUseCase, ListStudentAcademicRecordsUseCase>();
        services.AddScoped<IGetStudentAcademicRecordByStudentIdUseCase, GetStudentAcademicRecordByStudentIdUseCase>();
        services.AddScoped<IGetStudentTranscriptSummaryByStudentIdUseCase, GetStudentTranscriptSummaryByStudentIdUseCase>();
        services.AddScoped<IListAcademicOperationalAlertsUseCase, ListAcademicOperationalAlertsUseCase>();
        services.AddScoped<IListSectionOperationsSummariesUseCase, ListSectionOperationsSummariesUseCase>();
        services.AddScoped<IListTranscriptExportRowsUseCase, ListTranscriptExportRowsUseCase>();

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
        services.AddSingleton<IAcademicCatalogStore, FileAcademicCatalogStore>();
        services.AddSingleton<IAcademicCodeGenerator, AcademicCodeGenerator>();

        return services;
    }
}
