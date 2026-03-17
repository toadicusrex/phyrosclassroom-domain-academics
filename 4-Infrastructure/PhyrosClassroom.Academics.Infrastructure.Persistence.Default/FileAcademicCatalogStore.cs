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

    public Task<IReadOnlyList<CourseSection>> ListSectionsAsync(CancellationToken cancellationToken = default) =>
        ReadListAsync<CourseSection>(GetSectionsPath(), cancellationToken);

    public async Task<CourseSection> SaveSectionAsync(CourseSection section, CancellationToken cancellationToken = default)
    {
        var sections = (await ReadListAsync<CourseSection>(GetSectionsPath(), cancellationToken)).ToList();
        var index = sections.FindIndex(existing => existing.SectionId == section.SectionId);
        if (index >= 0)
        {
            sections[index] = section;
        }
        else
        {
            sections.Add(section);
        }

        await WriteListAsync(GetSectionsPath(), sections, cancellationToken);
        return section;
    }

    public async Task<IReadOnlyList<SectionRosterEntry>> ListRosterEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        (await ReadListAsync<SectionRosterEntry>(GetRosterPath(), cancellationToken))
            .Where(entry => entry.SectionId == sectionId)
            .ToList();

    public async Task<SectionRosterEntry> SaveRosterEntryAsync(SectionRosterEntry entry, CancellationToken cancellationToken = default)
    {
        var entries = (await ReadListAsync<SectionRosterEntry>(GetRosterPath(), cancellationToken)).ToList();
        var index = entries.FindIndex(existing => existing.SectionId == entry.SectionId && existing.StudentId == entry.StudentId);
        if (index >= 0)
        {
            entries[index] = entry;
        }
        else
        {
            entries.Add(entry);
        }

        await WriteListAsync(GetRosterPath(), entries, cancellationToken);
        return entry;
    }

    public async Task<IReadOnlyList<GradebookEntry>> ListGradebookEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        (await ReadListAsync<GradebookEntry>(GetGradebookPath(), cancellationToken))
            .Where(entry => entry.SectionId == sectionId)
            .ToList();

    public async Task<GradebookEntry> SaveGradebookEntryAsync(GradebookEntry entry, CancellationToken cancellationToken = default)
    {
        var entries = (await ReadListAsync<GradebookEntry>(GetGradebookPath(), cancellationToken)).ToList();
        var index = entries.FindIndex(existing =>
            existing.SectionId == entry.SectionId &&
            existing.StudentId == entry.StudentId &&
            existing.AssignmentId == entry.AssignmentId);
        if (index >= 0)
        {
            entries[index] = entry;
        }
        else
        {
            entries.Add(entry);
        }

        await WriteListAsync(GetGradebookPath(), entries, cancellationToken);
        return entry;
    }

    public async Task<IReadOnlyList<AttendanceEntry>> ListAttendanceEntriesAsync(Guid sectionId, CancellationToken cancellationToken = default) =>
        (await ReadListAsync<AttendanceEntry>(GetAttendancePath(), cancellationToken))
            .Where(entry => entry.SectionId == sectionId)
            .OrderByDescending(entry => entry.AttendanceDate)
            .ThenBy(entry => entry.StudentName)
            .ToList();

    public async Task<AttendanceEntry> SaveAttendanceEntryAsync(AttendanceEntry entry, CancellationToken cancellationToken = default)
    {
        var entries = (await ReadListAsync<AttendanceEntry>(GetAttendancePath(), cancellationToken)).ToList();
        var index = entries.FindIndex(existing =>
            existing.SectionId == entry.SectionId &&
            existing.StudentId == entry.StudentId &&
            existing.AttendanceDate == entry.AttendanceDate);
        if (index >= 0)
        {
            entries[index] = entry;
        }
        else
        {
            entries.Add(entry);
        }

        await WriteListAsync(GetAttendancePath(), entries, cancellationToken);
        return entry;
    }

    public async Task<IReadOnlyList<AssignmentSubmission>> ListAssignmentSubmissionsAsync(Guid sectionId, Guid assignmentId, CancellationToken cancellationToken = default) =>
        (await ReadListAsync<AssignmentSubmission>(GetSubmissionsPath(), cancellationToken))
            .Where(entry => entry.SectionId == sectionId && entry.AssignmentId == assignmentId)
            .OrderByDescending(entry => entry.SubmittedAtUtc)
            .ThenBy(entry => entry.StudentName)
            .ToList();

    public async Task<AssignmentSubmission> SaveAssignmentSubmissionAsync(AssignmentSubmission entry, CancellationToken cancellationToken = default)
    {
        var entries = (await ReadListAsync<AssignmentSubmission>(GetSubmissionsPath(), cancellationToken)).ToList();
        var index = entries.FindIndex(existing =>
            existing.SectionId == entry.SectionId &&
            existing.AssignmentId == entry.AssignmentId &&
            existing.StudentId == entry.StudentId);
        if (index >= 0)
        {
            entries[index] = entry;
        }
        else
        {
            entries.Add(entry);
        }

        await WriteListAsync(GetSubmissionsPath(), entries, cancellationToken);
        return entry;
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
    private string GetSectionsPath() => Path.Combine(_options.BasePath, "academic-sections.json");
    private string GetRosterPath() => Path.Combine(_options.BasePath, "academic-roster.json");
    private string GetGradebookPath() => Path.Combine(_options.BasePath, "academic-gradebook.json");
    private string GetAttendancePath() => Path.Combine(_options.BasePath, "academic-attendance.json");
    private string GetSubmissionsPath() => Path.Combine(_options.BasePath, "academic-submissions.json");
    private string GetRecordsPath() => Path.Combine(_options.BasePath, "academic-records.json");
}
