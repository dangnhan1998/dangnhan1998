using Hrms.Modules.Personnel.Contracts;
using Hrms.Modules.Personnel.Infrastructure;
using Hrms.Modules.Personnel.Infrastructure.Persistence;
using Hrms.Infrastructure.Modules;
using Hrms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Modules.Personnel;

/// <summary>
/// Module Nhân sự: Hồ sơ nhân sự, HĐLĐ và phụ lục, tái ký, chấm dứt HĐ, bổ nhiệm/miễn nhiệm, thống kê lao động.
/// </summary>
public sealed class PersonnelModule : IModule
{
    public const string SchemaName = "personnel";

    public string Name => "Nhân sự";

    public string Key => SchemaName;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<PersonnelDbContext>(configuration, SchemaName);
        services.AddScoped<IPersonnelModuleApi, PersonnelModuleApi>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoint của module tại đây, ví dụ: endpoints.MapGet("/...", ...).
        // Route thực tế sẽ là /api/personnel/...
    }
}
