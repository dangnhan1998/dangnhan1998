namespace Hrms.Mobile.WinUI;

/// <summary>Bản chạy trên Windows chỉ để dev thử nhanh giao diện, không phát hành.</summary>
public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
