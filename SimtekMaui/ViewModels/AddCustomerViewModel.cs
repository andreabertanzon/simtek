using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using SimtekMaui.Models;
using SimtekMaui.Models.Exceptions;
using SimtekMaui.Models.Query;
using SimtekMaui.Utils;
using SimtekMaui.Views;

namespace SimtekMaui.ViewModels;

public partial class AddCustomerViewModel : BaseViewModel
{
    private readonly IMediator _mediator;
    private readonly NewInterventionStateBuilder _stateBuilder;

    public AddCustomerViewModel(IMediator mediator, NewInterventionStateBuilder stateBuilder)
    {
        _mediator = mediator;
        _stateBuilder = stateBuilder;
    }

    [ObservableProperty] private Customer _newCustomer = new("", "", "");

    public ObservableRangeCollection<Customer> Customers { get; private set; } = new();

    [RelayCommand]
    public async Task LoadCustomers()
    {
        _stateBuilder.StartTracking();
        NewCustomer = new Customer("", "", "");

        var customers = await _mediator.Send(new GetCustomersQuery());
        customers.When(success => { Customers.AddRange(success); }, error => { InError = true; });
    }

    [RelayCommand]
    public async Task GoToAddIntervention()
    {
        const string actionButtonText = "OK";
        var duration = TimeSpan.FromSeconds(3);
        var cancellationTokenSource = new CancellationTokenSource();

        var validateAddressResult = ValidateAddress(NewCustomer.Address);
        if (!validateAddressResult.IsSuccess)
        {
            var error = validateAddressResult.Error;
            var snackBar =
                SnackbarFactory.MakeSnackBar(SnackbarType.Error, error.Message, actionButtonText, duration);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }

        if (string.IsNullOrWhiteSpace(NewCustomer.Name) || string.IsNullOrWhiteSpace(NewCustomer.Surname))
        {
            const string text = "Nome, Cognome, Indirizzo Obbligatori";

            var snackBar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, text, actionButtonText, duration);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }

        var result = _stateBuilder.AddCustomerData(NewCustomer);
        if (!result.IsSuccess)
        {
            const string errorText = "Errore nell'aggiunta del Cantiere";
            var errorSnackbar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, errorText, actionButtonText, duration);
            await errorSnackbar.Show(cancellationTokenSource.Token);
        }

        await Shell.Current.GoToAsync(nameof(AddSitePage), true);
    }
}