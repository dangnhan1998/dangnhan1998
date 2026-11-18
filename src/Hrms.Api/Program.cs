using Hrms.Infrastructure.Modules;
using Hrms.Modules.CnB;
using Hrms.Modules.Foundation;
using Hrms.Modules.Personnel;
using Hrms.Modules.Recruitment;
using Hrms.Modules.Training;
using Hrms.UI;
using Hrms.Web.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) => logger.ReadFrom.Configuration(context.Configuration));

// Module nghiệp vụ: thêm module mới thì khai báo tại đây.
builder.Services.AddModules(
    builder.Configuration,
    new FoundationModule(),
    new RecruitmentModule(),
    new PersonnelModule(),
    new TrainingModule(),
    new CnBModule());

// API cho App MAUI
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

// Giao diện Web (Blazor Interactive Server)
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddHrmsUi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapHealthChecks("/health");
app.MapGet("/api/modules", (IEnumerable<IModule> modules) =>
        modules.Select(m => new { m.Key, m.Name }))
    .WithTags("System");
app.MapModules();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Hrms.UI.Layout.MainLayout).Assembly);

app.Run();

/// <summary>Để Hrms.IntegrationTests dùng được WebApplicationFactory.</summary>
public partial class Program;
