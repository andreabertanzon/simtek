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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _mainViewModel.LoadInterventionsAsync();
    }
}