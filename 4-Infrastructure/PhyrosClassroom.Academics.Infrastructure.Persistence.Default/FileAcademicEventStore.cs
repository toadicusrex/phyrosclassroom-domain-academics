using System.Text.Json;
using Microsoft.Extensions.Options;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default;

public sealed class FileAcademicEventStore(IOptions<AcademicStorageOptions> options) : IAcademicEventStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
    };

    private static readonly SemaphoreSlim Gate = new(1, 1);

    public async Task AppendAsync(
        Guid academicId,
        IReadOnlyList<AcademicEventRecord> events,
        CancellationToken cancellationToken = default)
    {
        var filePath = GetEventsPath(options.Value.BasePath, academicId);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            var existingEvents = await ReadInternalAsync(filePath, cancellationToken);
            existingEvents.AddRange(events);

            await using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, existingEvents, SerializerOptions, cancellationToken);
        }
        finally
        {
            Gate.Release();
        }
    }

    public async Task<IReadOnlyList<AcademicEventRecord>> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        var filePath = GetEventsPath(options.Value.BasePath, academicId);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            return await ReadInternalAsync(filePath, cancellationToken);
        }
        finally
        {
            Gate.Release();
        }
    }

    private static string GetEventsPath(string basePath, Guid academicId)
    {
        return Path.Combine(basePath, "academics", "events", $"{academicId:N}.json");
    }

    private static async Task<List<AcademicEventRecord>> ReadInternalAsync(string filePath, CancellationToken cancellationToken)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        await using var stream = File.OpenRead(filePath);
        var events = await JsonSerializer.DeserializeAsync<List<AcademicEventRecord>>(stream, SerializerOptions, cancellationToken);
        return events ?? [];
    }
}
