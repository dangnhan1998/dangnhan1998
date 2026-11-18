using Hrms.Modules.Recruitment.Contracts;
using Hrms.Modules.Recruitment.Infrastructure;
using Hrms.Modules.Recruitment.Infrastructure.Persistence;
using Hrms.Infrastructure.Modules;
using Hrms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Modules.Recruitment;

/// <summary>
/// Module Tuyển dụng: Định biên, yêu cầu tuyển dụng, ATS (ứng viên, phỏng vấn, Offer), Onboarding, báo cáo tuyển dụng.
/// </summary>
public sealed class RecruitmentModule : IModule
{
    public const string SchemaName = "recruitment";

    public string Name => "Tuyển dụng";

    public string Key => SchemaName;

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<RecruitmentDbContext>(configuration, SchemaName);
        services.AddScoped<IRecruitmentModuleApi, RecruitmentModuleApi>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoint của module tại đây, ví dụ: endpoints.MapGet("/...", ...).
        // Route thực tế sẽ là /api/recruitment/...
    }
}
