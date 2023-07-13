using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using SimtekMaui.Models;
using SimtekMaui.Models.Query;
using SimtekMaui.Utils;
using CommunityToolkit.Maui.Alerts;
using SimtekMaui.Views;

namespace SimtekMaui.ViewModels;

public partial class AddCustomerViewModel : BaseViewModel
{
    private readonly IMediator _mediator;

    public AddCustomerViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ObservableProperty] bool makeNew;

    [ObservableProperty] Customer newCustomer = new("", "", "");
    //public string Email { get; set; } = string.Empty;
    //public string Phone { get; set; } = string.Empty;
    //public string Vat { get; set; } = string.Empty;

    public ObservableRangeCollection<Customer> Customers { get; private set; } = new();

    [RelayCommand]
    public async Task LoadCustomers()
    {
        NewCustomer = new Customer("", "", "");
        MakeNew = false;

        var customers = await _mediator.Send(new GetCustomersQuery());
        customers.When(success => { Customers.AddRange(success); }, error => { InError = true; });
    }

    [RelayCommand]
    public async Task GoToAddIntervention()
    {
        if (string.IsNullOrWhiteSpace(NewCustomer.Name) || string.IsNullOrWhiteSpace(NewCustomer.Surname) || string.IsNullOrWhiteSpace(NewCustomer.Address))
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string text = "Nome, Cognome, Indirizzo Obbligatori";
            string actionButtonText = "OK";
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackBar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, text, actionButtonText, duration);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }

        await Shell.Current.GoToAsync(nameof(AddInterventionPage), true, new Dictionary<string, object>()
        {
            { "Customer", NewCustomer }
        });
    }
}