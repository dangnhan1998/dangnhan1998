using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Infrastructure.Modules;

public static class ModuleExtensions
{
    /// <summary>Đăng ký service của các module và lưu danh sách module vào DI.</summary>
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration,
        params IModule[] modules)
    {
        foreach (var module in modules)
        {
            module.RegisterServices(services, configuration);
            services.AddSingleton(module);
        }

        return services;
    }

    /// <summary>Map endpoint của mọi module dưới <c>/api/{module.Key}</c>.</summary>
    public static IEndpointRouteBuilder MapModules(this IEndpointRouteBuilder endpoints)
    {
        foreach (var module in endpoints.ServiceProvider.GetServices<IModule>())
        {
            var group = endpoints.MapGroup($"/api/{module.Key}").WithTags(module.Name);
            module.MapEndpoints(group);
        }

        return endpoints;
    }
}
