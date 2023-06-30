using SimtekMaui.ViewModels;

namespace SimtekMaui.Views;

public partial class AddCustomerPage : ContentPage
{
	private readonly AddCustomerViewModel _viewModel;
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
	}
}