using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using SimtekMaui.ViewModels;

namespace SimtekMaui;

public partial class MainPage : ContentPage
{

    private readonly MainViewModel _mainViewModel;

    public MainPage(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        InitializeComponent();
        BindingContext = mainViewModel;

        statusBar
            .SetAppTheme(StatusBarBehavior.StatusBarStyleProperty, StatusBarStyle.DarkContent, StatusBarStyle.LightContent);
        statusBar
            .SetAppThemeColor(StatusBarBehavior.StatusBarColorProperty, Colors.Transparent, Colors.Transparent);
        //if (MicrosoftMaui.Current.RequestedTheme == ApplicationTheme.Light)
        //{
        //    statusBar.StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.LightContent;
        //    statusBar.StatusBarColor = (Color)Resources["PrimaryLight"];
        //}
        //else
        //{
        //    statusBar.StatusBarColor = (Color)Resources["PrimaryDark"];
        //    statusBar.StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.DarkContent;
        //}
    }
    

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //if (Microsoft.UI.Xaml.Application.Current.RequestedTheme == ApplicationTheme.Dark)
        //{
            
        //}

        await _mainViewModel.LoadInterventionsAsync();
    }
    
}