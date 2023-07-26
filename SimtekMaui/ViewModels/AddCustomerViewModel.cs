using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using SimtekMaui.Models;
using SimtekMaui.Models.Exceptions;
using SimtekMaui.Models.Query;
using SimtekMaui.Utils;
using SimtekMaui.Views;
using System.Collections.ObjectModel;

namespace SimtekMaui.ViewModels;

public partial class AddCustomerViewModel : BaseViewModel
{
    private readonly IMediator _mediator;
    private readonly NewInterventionStateBuilder _stateBuilder;

    public delegate void ShowBottomSheetHandler();

    public event ShowBottomSheetHandler? ShowBottomSheetEvent;

    public AddCustomerViewModel(IMediator mediator, NewInterventionStateBuilder stateBuilder)
    {
        _mediator = mediator;
        _stateBuilder = stateBuilder;
    }

    [ObservableProperty] private Customer _newCustomer = new("", "", "");

    public ObservableRangeCollection<Customer> Customers { get; private set; } = new();


    [RelayCommand]
    public async Task LoadCustomersAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();

        IsBusy = true;
        
        _stateBuilder.StartTracking();
        NewCustomer = new Customer("", "", "");

        var customers = await _mediator.Send(new GetCustomersQuery(), cancellationTokenSource.Token);
        customers.When(
            success => { Customers.AddRange(success); },
            error => { InError = true; });
        
        IsBusy = false;
    }

    [RelayCommand]
    public async Task GoToAddIntervention()
    {
        const string actionButtonText = "OK";
        var duration = TimeSpan.FromSeconds(3);
        var cancellationTokenSource = new CancellationTokenSource();

        if(string.IsNullOrWhiteSpace(NewCustomer.Name) || string.IsNullOrWhiteSpace(NewCustomer.Surname))
        {
            const string text = "Nome, Cognome, Indirizzo Obbligatori";

            var snackBar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, text);
            
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }
        
        var validateAddressResult = ValidateAddress(NewCustomer.Address);
        if (!validateAddressResult.IsSuccess)
        {
            var error = validateAddressResult.Error;
            var snackBar =
                SnackbarFactory.MakeSnackBar(SnackbarType.Error, error.Message);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }

        var result = _stateBuilder.AddCustomerData(NewCustomer);
        if (!result.IsSuccess)
        {
            const string errorText = "Errore nell'aggiunta del Cantiere";
            var errorSnackbar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, errorText);
            await errorSnackbar.Show(cancellationTokenSource.Token);
        }

        await Shell.Current.GoToAsync(nameof(AddSitePage), true);
    }

    [RelayCommand]
    public void OpenBottomSheet()
    {
        ShowBottomSheetEvent?.Invoke();
    }
}