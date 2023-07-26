using SimtekMaui.PageUtils;
using SimtekMaui.ViewModels;
using The49.Maui.BottomSheet;

namespace SimtekMaui.Views;

public partial class AddCustomerPage : ContentPage
{
    private readonly AddCustomerViewModel _viewModel;
    private bool showingModal = false;

    public AddCustomerPage(AddCustomerViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCustomers();
        _viewModel.ShowBottomSheetEvent += OpenModalSheet;
    }

    private void FinishedShowingModal(object? obj,DismissOrigin origin) => showingModal = false;
    
    private async void OpenModalSheet()
    {
        if (showingModal) return;

        showingModal = true;
        var page = new BottomCustomerInfo(_viewModel)
        {
            HasBackdrop = true
        };
        if (Window is not null)
        {
            await page.ShowAsync(Window);
        }

        page.Dismissed += FinishedShowingModal;
    }
}