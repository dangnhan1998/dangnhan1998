using Hrms.Modules.CnB.Contracts;
using Hrms.Modules.CnB.Infrastructure;
using Hrms.Modules.CnB.Infrastructure.Persistence;
using Hrms.Infrastructure.Modules;
using Hrms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Modules.CnB;

/// <summary>
/// Module C&B: Bảo hiểm, chấm công và ca, nghỉ phép, tăng ca, tiền lương, NPT, công đoàn, Chatbot AI và thư viện điện tử.
/// </summary>
public sealed class CnBModule : IModule
{
    public const string SchemaName = "cnb";

    public string Name => "C&B";

    public string Key => SchemaName;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<CnBDbContext>(configuration, SchemaName);
        services.AddScoped<ICnBModuleApi, CnBModuleApi>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoint của module tại đây, ví dụ: endpoints.MapGet("/...", ...).
        // Route thực tế sẽ là /api/cnb/...
    }
}
