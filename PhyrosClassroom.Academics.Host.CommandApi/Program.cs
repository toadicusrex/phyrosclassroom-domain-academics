using PhyrosClassroom.Academics.Composition;
using PhyrosClassroom.Academics.Presentation.CommandApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAcademicCommandServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    service = "phyrosclassroom-domain-academics-command",
    status = "ok",
    timestampUtc = DateTimeOffset.UtcNow,
}));
app.MapAcademicCommandApi();

app.Run();
