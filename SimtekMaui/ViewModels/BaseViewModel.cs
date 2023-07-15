using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using SimtekMaui.Models;
using SimtekMaui.Models.Exceptions;

namespace SimtekMaui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty] private bool _inError;

        [ObservableProperty] string _title = "";

        public bool IsNotBusy => !IsBusy;

        internal static Result<Unit> ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return Result<Unit>.Failure(new ValidationException("Indirizzo obbligatorio"));
            }

            if (address.Split(",").Length < 3)
            {
                return Result<Unit>.Failure(
                    new ValidationException("Indirizzo non valido, scrivi con le virgole:\n via, citta', provincia"));
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}