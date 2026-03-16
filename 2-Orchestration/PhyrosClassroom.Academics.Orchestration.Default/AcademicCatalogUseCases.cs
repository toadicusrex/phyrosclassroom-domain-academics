using PhyrosClassroom.Academics.Infrastructure.Persistence;
using PhyrosClassroom.Academics.Models;
using PhyrosClassroom.Academics.Orchestration;

namespace PhyrosClassroom.Academics.Orchestration.Default;

public sealed class ListAcademicCoursesUseCase(IAcademicCatalogStore store) : IListAcademicCoursesUseCase
{
    public Task<IReadOnlyList<CourseCatalogEntry>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListCoursesAsync(cancellationToken);
}

public sealed class SaveAcademicCourseUseCase(IAcademicCatalogStore store) : ISaveAcademicCourseUseCase
{
    public Task<CourseCatalogEntry> ExecuteAsync(SaveCourseCatalogEntryRequest request, CancellationToken cancellationToken = default) =>
        store.SaveCourseAsync(
            new CourseCatalogEntry
            {
                CourseId = request.CourseId,
                CourseCode = request.CourseCode.Trim(),
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Department = request.Department.Trim(),
                InstructorName = request.InstructorName.Trim(),
                DeliveryModel = request.DeliveryModel.Trim(),
                GradeBand = request.GradeBand.Trim(),
                EnrollmentOpen = request.EnrollmentOpen,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListAcademicAssignmentsUseCase(IAcademicCatalogStore store) : IListAcademicAssignmentsUseCase
{
    public Task<IReadOnlyList<CourseworkAssignment>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListAssignmentsAsync(cancellationToken);
}

public sealed class SaveAcademicAssignmentUseCase(IAcademicCatalogStore store) : ISaveAcademicAssignmentUseCase
{
    public Task<CourseworkAssignment> ExecuteAsync(SaveCourseworkAssignmentRequest request, CancellationToken cancellationToken = default) =>
        store.SaveAssignmentAsync(
            new CourseworkAssignment
            {
                AssignmentId = request.AssignmentId,
                CourseId = request.CourseId,
                CourseTitle = request.CourseTitle.Trim(),
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                DueDate = request.DueDate,
                Audience = request.Audience.Trim(),
                PublishedByUserId = request.PublishedByUserId.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}

public sealed class ListStudentAcademicRecordsUseCase(IAcademicCatalogStore store) : IListStudentAcademicRecordsUseCase
{
    public Task<IReadOnlyList<StudentAcademicRecord>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        store.ListRecordsAsync(cancellationToken);
}

public sealed class GetStudentAcademicRecordByStudentIdUseCase(IAcademicCatalogStore store) : IGetStudentAcademicRecordByStudentIdUseCase
{
    public Task<StudentAcademicRecord?> ExecuteAsync(Guid studentId, CancellationToken cancellationToken = default) =>
        store.GetRecordByStudentIdAsync(studentId, cancellationToken);
}

public sealed class SaveStudentAcademicRecordUseCase(IAcademicCatalogStore store) : ISaveStudentAcademicRecordUseCase
{
    public Task<StudentAcademicRecord> ExecuteAsync(SaveStudentAcademicRecordRequest request, CancellationToken cancellationToken = default) =>
        store.SaveRecordAsync(
            new StudentAcademicRecord
            {
                StudentId = request.StudentId,
                StudentCode = request.StudentCode.Trim(),
                StudentName = request.StudentName.Trim(),
                GradeLevel = request.GradeLevel.Trim(),
                Courses = request.Courses.ToList(),
                TranscriptTerms = request.TranscriptTerms.ToList(),
                CumulativeGpa = request.CumulativeGpa,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            },
            cancellationToken);
}
