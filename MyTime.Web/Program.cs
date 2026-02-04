using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shifts;
using MyTime.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();


var connectionString = builder.Configuration.GetConnectionString("mytimedb");
builder.Services.AddMyTimeDbContext(connectionString!, builder.Environment.IsDevelopment());

builder.Services.AddScoped<IPunchService, PunchService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MyTimeDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
