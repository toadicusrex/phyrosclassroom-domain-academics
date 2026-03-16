using Microsoft.Extensions.Options;
using PhyrosClassroom.Academics.Infrastructure.Persistence.Default;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default.Tests;

public sealed class FileAcademicReadModelStoreTests
{
    [Fact]
    public async Task UpsertAsync_PersistsAndReturnsAcademic()
    {
        var basePath = Path.Combine(
            Path.GetTempPath(),
            "phyrosclassroom-academics-infrastructure-tests",
            Guid.NewGuid().ToString("N"));

        var store = new FileAcademicReadModelStore(
            Options.Create(new AcademicStorageOptions
            {
                BasePath = basePath,
            }));

        var academic = new AcademicReadModel(
            Guid.NewGuid(),
            "AB-20260228083045",
            "Alice",
            "Bennett",
            new DateTimeOffset(2026, 2, 28, 8, 30, 45, TimeSpan.Zero),
            "registrations:default",
            "registration-123:child:0",
            new DateOnly(2012, 4, 16),
            "6",
            "Sarah Bennett",
            "sarah@example.com",
            false,
            null,
            false,
            [],
            []);

        await store.UpsertAsync(academic);
        var loadedAcademic = await store.GetByIdAsync(academic.AcademicId);

        Assert.NotNull(loadedAcademic);
        Assert.Equal(academic.AcademicCode, loadedAcademic!.AcademicCode);
    }
}
