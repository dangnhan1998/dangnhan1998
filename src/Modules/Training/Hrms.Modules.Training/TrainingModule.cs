using Hrms.Modules.Training.Contracts;
using Hrms.Modules.Training.Infrastructure;
using Hrms.Modules.Training.Infrastructure.Persistence;
using Hrms.Infrastructure.Modules;
using Hrms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Modules.Training;

/// <summary>
/// Module Đào tạo: Nhu cầu và kế hoạch đào tạo, lớp học, E-Learning, thi, cam kết đào tạo, chứng chỉ.
/// </summary>
public sealed class TrainingModule : IModule
{
    public const string SchemaName = "training";

    public string Name => "Đào tạo";

    public string Key => SchemaName;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<TrainingDbContext>(configuration, SchemaName);
        services.AddScoped<ITrainingModuleApi, TrainingModuleApi>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoint của module tại đây, ví dụ: endpoints.MapGet("/...", ...).
        // Route thực tế sẽ là /api/training/...
    }
}
