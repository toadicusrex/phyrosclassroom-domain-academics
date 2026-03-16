using PhyrosClassroom.Academics.Engines.Default;

namespace PhyrosClassroom.Academics.Engines.Default.Tests;

public sealed class AcademicCodeGeneratorTests
{
    [Fact]
    public void GenerateCode_UsesInitialsAndTimestamp()
    {
        var generator = new AcademicCodeGenerator();

        var code = generator.GenerateCode(
            "Alice",
            "Bennett",
            new DateTimeOffset(2026, 2, 28, 8, 30, 45, TimeSpan.Zero));

        Assert.Equal("AB-20260228083045", code);
    }
}
