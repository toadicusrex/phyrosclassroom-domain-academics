using PhyrosClassroom.Academics.Composition;
using PhyrosClassroom.Academics.Presentation.QueryApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAcademicQueryServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    service = "phyrosclassroom-domain-academics-query",
    status = "ok",
    timestampUtc = DateTimeOffset.UtcNow,
}));
app.MapAcademicQueryApi();

app.Run();
