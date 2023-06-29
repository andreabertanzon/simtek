using SimtekMaui.ViewModels;

namespace SimtekMaui.Views;

public partial class AddInterventionPage : ContentPage
{
    private readonly AddInterventionViewModel _viewModel;

    public AddInterventionPage(AddInterventionViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}