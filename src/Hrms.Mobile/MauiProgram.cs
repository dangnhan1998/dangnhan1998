using Hrms.UI;
using Microsoft.Extensions.Logging;

namespace Hrms.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddHrmsUi();

        // TODO: đăng ký HttpClient trỏ tới Hrms.Api, lưu token đăng nhập bằng SecureStorage.

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
