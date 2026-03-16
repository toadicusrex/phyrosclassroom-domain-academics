using System.Text.Json;
using Microsoft.Extensions.Options;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default;

public sealed class FileAcademicReadModelStore(IOptions<AcademicStorageOptions> options) : IAcademicReadModelStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
    };

    private static readonly SemaphoreSlim Gate = new(1, 1);

    public async Task UpsertAsync(AcademicReadModel academic, CancellationToken cancellationToken = default)
    {
        var filePath = GetReadModelsPath(options.Value.BasePath);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            var academics = await ReadInternalAsync(filePath, cancellationToken);
            academics[academic.AcademicId] = academic;

            await using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(
                stream,
                academics.Values.OrderBy(item => item.AcademicCode).ToList(),
                SerializerOptions,
                cancellationToken);
        }
        finally
        {
            Gate.Release();
        }
    }

    public async Task<AcademicReadModel?> GetByIdAsync(Guid academicId, CancellationToken cancellationToken = default)
    {
        var filePath = GetReadModelsPath(options.Value.BasePath);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            var academics = await ReadInternalAsync(filePath, cancellationToken);
            return academics.TryGetValue(academicId, out var academic) ? academic : null;
        }
        finally
        {
            Gate.Release();
        }
    }

    public async Task<AcademicReadModel?> GetBySourceReferenceAsync(string sourceSystem, string sourceReference, CancellationToken cancellationToken = default)
    {
        var filePath = GetReadModelsPath(options.Value.BasePath);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            return (await ReadInternalAsync(filePath, cancellationToken))
                .Values
                .FirstOrDefault(item =>
                    string.Equals(item.SourceSystem, sourceSystem, StringComparison.Ordinal) &&
                    string.Equals(item.SourceReference, sourceReference, StringComparison.Ordinal));
        }
        finally
        {
            Gate.Release();
        }
    }

    public async Task<IReadOnlyList<AcademicReadModel>> ListAsync(CancellationToken cancellationToken = default)
    {
        var filePath = GetReadModelsPath(options.Value.BasePath);

        await Gate.WaitAsync(cancellationToken);
        try
        {
            return (await ReadInternalAsync(filePath, cancellationToken))
                .Values
                .OrderBy(item => item.AcademicCode)
                .ToList();
        }
        finally
        {
            Gate.Release();
        }
    }

    private static string GetReadModelsPath(string basePath)
    {
        return Path.Combine(basePath, "academics", "read-models.json");
    }

    private static async Task<Dictionary<Guid, AcademicReadModel>> ReadInternalAsync(string filePath, CancellationToken cancellationToken)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        await using var stream = File.OpenRead(filePath);
        var academics = await JsonSerializer.DeserializeAsync<List<AcademicReadModel>>(stream, SerializerOptions, cancellationToken);
        return (academics ?? []).ToDictionary(item => item.AcademicId);
    }
}
