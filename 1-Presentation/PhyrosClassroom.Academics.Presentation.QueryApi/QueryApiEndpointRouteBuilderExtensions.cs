using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PhyrosClassroom.Academics.Orchestration;

namespace PhyrosClassroom.Academics.Presentation.QueryApi;

public static class QueryApiEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAcademicQueryApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/query/academics");

        group.MapGet("/", async (
            IListAcademicsUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academics = await useCase.ExecuteAsync(cancellationToken);
            return Results.Ok(academics);
        });

        group.MapGet("/{academicId:guid}", async (
            Guid academicId,
            IGetAcademicReadModelByIdUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(academicId, cancellationToken);
            return academic is null ? Results.NotFound() : Results.Ok(academic);
        });

        group.MapGet("/by-subject/{subjectId}", async (
            string subjectId,
            IGetAcademicReadModelBySubjectIdUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var academic = await useCase.ExecuteAsync(subjectId, cancellationToken);
            return academic is null ? Results.NotFound() : Results.Ok(academic);
        });

        group.MapGet("/courses", async (
            IListAcademicCoursesUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var courses = await useCase.ExecuteAsync(cancellationToken);
            return Results.Ok(courses);
        });

        group.MapGet("/assignments", async (
            IListAcademicAssignmentsUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var assignments = await useCase.ExecuteAsync(cancellationToken);
            return Results.Ok(assignments);
        });

        group.MapGet("/records", async (
            IListStudentAcademicRecordsUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var records = await useCase.ExecuteAsync(cancellationToken);
            return Results.Ok(records);
        });

        group.MapGet("/records/{studentId:guid}", async (
            Guid studentId,
            IGetStudentAcademicRecordByStudentIdUseCase useCase,
            CancellationToken cancellationToken) =>
        {
            var record = await useCase.ExecuteAsync(studentId, cancellationToken);
            return record is null ? Results.NotFound() : Results.Ok(record);
        });

        return endpoints;
    }
}
