using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimtekMaui.Models;
using SimtekMaui.Utils;
using SimtekMaui.Views;

namespace SimtekMaui.ViewModels;

public partial class AddSiteViewModel : BaseViewModel
{
    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _address = "";
    private readonly NewInterventionStateBuilder _stateBuilder;

    public AddSiteViewModel(NewInterventionStateBuilder stateBuilder)
    {
        _stateBuilder = stateBuilder;
    }

    [RelayCommand]
    public async Task GoToAddIntervention()
    {
        const string actionButtonText = "OK";
        var duration = TimeSpan.FromSeconds(3);
        var cancellationTokenSource = new CancellationTokenSource();

        var validateAddress = ValidateAddress(Address);
        if (!validateAddress.IsSuccess)
        {
            var snackBar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, validateAddress.Error.Message, actionButtonText, duration);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }
        
        if (string.IsNullOrWhiteSpace(Name))
        {
            const string text = "Nome e Indirizzo Obbligatori";

            var snackBar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, text, actionButtonText, duration);
            await snackBar.Show(cancellationTokenSource.Token);
            return;
        }

        var result = _stateBuilder.AddSiteData(Name, Address);
        if (!result.IsSuccess)
        {
            const string errorText = "Errore nell'aggiunta del Cliente";
            var errorSnackbar = SnackbarFactory.MakeSnackBar(SnackbarType.Error, errorText, actionButtonText, duration);
            await errorSnackbar.Show(cancellationTokenSource.Token);
        }

        await Shell.Current.GoToAsync(nameof(AddInterventionPage), true);
    }

    [RelayCommand]
    public void AddAddressFromCustomer()
    {
        Address = _stateBuilder.GetAddressFromCustomer();
    }
}