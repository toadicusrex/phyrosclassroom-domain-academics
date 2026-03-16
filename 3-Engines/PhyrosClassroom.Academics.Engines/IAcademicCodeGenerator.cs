namespace PhyrosClassroom.Academics.Engines;

public interface IAcademicCodeGenerator
{
    string GenerateCode(string givenName, string familyName, DateTimeOffset occurredUtc);
}
