#if __ANDROID__
using Android.Content.Res;
#endif

namespace SimtekMaui;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Pippo", (handler, view) =>
        {
#if __ANDROID__
            //handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            //handler.PlatformView.SetHighlightColor(Android.Graphics.Color.Transparent);
            //handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf();
#elif __IOS__
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
    }
}