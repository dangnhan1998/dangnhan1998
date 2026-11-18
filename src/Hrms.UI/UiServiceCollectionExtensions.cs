using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Hrms.UI;

public static class UiServiceCollectionExtensions
{
    /// <summary>Đăng ký service giao diện dùng chung. Gọi ở cả Hrms.Api (Web) và Hrms.Mobile.</summary>
    public static IServiceCollection AddHrmsUi(this IServiceCollection services)
    {
        services.AddMudServices();
        return services;
    }
}
