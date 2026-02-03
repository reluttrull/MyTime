using Microsoft.EntityFrameworkCore;
using MyTime.ApiService;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shifts;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("mytimedb");
builder.Services.AddMyTimeDbContext(connectionString!, builder.Environment.IsDevelopment());

builder.Services.AddScoped<IPunchService, PunchService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MyTimeDbContext>();
    db.Database.Migrate();
}

app.MapGet("/punches/me", async (IPunchService service, CancellationToken ct) =>
{
    var mockPunches = await service.GetAllPunchesAsync(1, token: ct);
    return mockPunches.Select(p => p.MapToResponse());
})
.WithName("GetAllPunches");

app.MapDefaultEndpoints();

app.Run();