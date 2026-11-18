using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Infrastructure.Modules;

/// <summary>
/// Điểm vào của một module nghiệp vụ. Mỗi module tự đăng ký service và endpoint của mình,
/// Hrms.Api chỉ cần liệt kê các module trong <c>Program.cs</c>.
/// </summary>
public interface IModule
{
    /// <summary>Tên hiển thị của module, ví dụ "Tuyển dụng".</summary>
    string Name { get; }

    /// <summary>Tiền tố route API và tên schema SQL Server của module, ví dụ "recruitment".</summary>
    string Key { get; }

    void RegisterServices(IServiceCollection services, IConfiguration configuration);

    void MapEndpoints(IEndpointRouteBuilder endpoints);
}
