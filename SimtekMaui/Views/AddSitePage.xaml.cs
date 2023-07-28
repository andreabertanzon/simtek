using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimtekMaui.PageUtils;
using SimtekMaui.ViewModels;
using The49.Maui.BottomSheet;

namespace SimtekMaui.Views;

public partial class AddSitePage : ContentPage
{
    private readonly AddSiteViewModel _viewModel;
    private bool _showingModal = false;
    public AddSitePage(AddSiteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSiteAsync();
        _viewModel.ShowBottomSheetEvent += OpenModalSheet;
    }

    private void FinishedShowingModal(object? obj,DismissOrigin origin) => _showingModal = false;
    
    private async void OpenModalSheet()
    {
        if (_showingModal) return;

        _showingModal = true;
        var page = new BottomSiteInfo(_viewModel)
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