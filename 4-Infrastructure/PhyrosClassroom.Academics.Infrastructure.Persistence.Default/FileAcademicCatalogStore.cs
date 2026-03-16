using System.Text.Json;
using Microsoft.Extensions.Options;
using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;

namespace PhyrosClassroom.Academics.Infrastructure.Persistence.Default;

public sealed class FileAcademicCatalogStore(IOptions<AcademicStorageOptions> options) : IAcademicCatalogStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
    };

    private readonly AcademicStorageOptions _options = options.Value;

    public Task<IReadOnlyList<CourseCatalogEntry>> ListCoursesAsync(CancellationToken cancellationToken = default) =>
        ReadListAsync<CourseCatalogEntry>(GetCoursesPath(), cancellationToken);

    public async Task<CourseCatalogEntry> SaveCourseAsync(CourseCatalogEntry course, CancellationToken cancellationToken = default)
    {
        var courses = (await ReadListAsync<CourseCatalogEntry>(GetCoursesPath(), cancellationToken)).ToList();
        var index = courses.FindIndex(existing => existing.CourseId == course.CourseId);
        if (index >= 0)
        {
            courses[index] = course;
        }
        else
        {
            courses.Add(course);
        }

        await WriteListAsync(GetCoursesPath(), courses, cancellationToken);
        return course;
    }

    public Task<IReadOnlyList<CourseworkAssignment>> ListAssignmentsAsync(CancellationToken cancellationToken = default) =>
        ReadListAsync<CourseworkAssignment>(GetAssignmentsPath(), cancellationToken);

    public async Task<CourseworkAssignment> SaveAssignmentAsync(CourseworkAssignment assignment, CancellationToken cancellationToken = default)
    {
        var assignments = (await ReadListAsync<CourseworkAssignment>(GetAssignmentsPath(), cancellationToken)).ToList();
        var index = assignments.FindIndex(existing => existing.AssignmentId == assignment.AssignmentId);
        if (index >= 0)
        {
            assignments[index] = assignment;
        }
        else
        {
            assignments.Add(assignment);
        }

        await WriteListAsync(GetAssignmentsPath(), assignments, cancellationToken);
        return assignment;
    }

    public Task<IReadOnlyList<StudentAcademicRecord>> ListRecordsAsync(CancellationToken cancellationToken = default) =>
        ReadListAsync<StudentAcademicRecord>(GetRecordsPath(), cancellationToken);

    public async Task<StudentAcademicRecord?> GetRecordByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default) =>
        (await ReadListAsync<StudentAcademicRecord>(GetRecordsPath(), cancellationToken))
        .FirstOrDefault(record => record.StudentId == studentId);

    public async Task<StudentAcademicRecord> SaveRecordAsync(StudentAcademicRecord record, CancellationToken cancellationToken = default)
    {
        var records = (await ReadListAsync<StudentAcademicRecord>(GetRecordsPath(), cancellationToken)).ToList();
        var index = records.FindIndex(existing => existing.StudentId == record.StudentId);
        if (index >= 0)
        {
            records[index] = record;
        }
        else
        {
            records.Add(record);
        }

        await WriteListAsync(GetRecordsPath(), records, cancellationToken);
        return record;
    }

    private async Task<IReadOnlyList<T>> ReadListAsync<T>(string path, CancellationToken cancellationToken)
    {
        EnsureBasePath();
        if (!File.Exists(path))
        {
            return [];
        }

        await using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream, SerializerOptions, cancellationToken) ?? [];
    }

    private async Task WriteListAsync<T>(string path, List<T> items, CancellationToken cancellationToken)
    {
        EnsureBasePath();
        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, items, SerializerOptions, cancellationToken);
    }

    private void EnsureBasePath() => Directory.CreateDirectory(_options.BasePath);

    private string GetCoursesPath() => Path.Combine(_options.BasePath, "academic-courses.json");
    private string GetAssignmentsPath() => Path.Combine(_options.BasePath, "academic-assignments.json");
    private string GetRecordsPath() => Path.Combine(_options.BasePath, "academic-records.json");
}
