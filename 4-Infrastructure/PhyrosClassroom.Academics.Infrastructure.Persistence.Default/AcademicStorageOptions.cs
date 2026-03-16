namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default;

public sealed class AcademicStorageOptions
{
    public const string SectionName = "AcademicStorage";

    public string BasePath { get; set; } = "App_Data";
}
