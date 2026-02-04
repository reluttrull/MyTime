using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shifts;
using MyTime.Shifts.Contracts.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.Run();