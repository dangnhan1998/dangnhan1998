using Hrms.Modules.Foundation.Contracts;
using Hrms.Modules.Foundation.Infrastructure;
using Hrms.Modules.Foundation.Infrastructure.Persistence;
using Hrms.Infrastructure.Modules;
using Hrms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Modules.Foundation;

/// <summary>
/// Module Nền tảng: Cơ cấu tổ chức, danh mục dùng chung, tài khoản và phân quyền (ASP.NET Core Identity), cấu hình ma trận phê duyệt, thông báo.
/// </summary>
public sealed class FoundationModule : IModule
{
    public const string SchemaName = "foundation";

    public string Name => "Nền tảng";

    public string Key => SchemaName;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<FoundationDbContext>(configuration, SchemaName);
        services.AddScoped<IFoundationModuleApi, FoundationModuleApi>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoint của module tại đây, ví dụ: endpoints.MapGet("/...", ...).
        // Route thực tế sẽ là /api/foundation/...
    }
}
