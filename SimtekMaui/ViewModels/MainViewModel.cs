using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using SimtekMaui.Utils;
using CommunityToolkit.Mvvm.Input;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Models;
using SimtekMaui.Views;
using SimtekMaui.Models.Query;

namespace SimtekMaui.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly IInterventionRepository _interventionRepository;
        private readonly IMediator _mediator;

        public MainViewModel(IInterventionRepository interventionRepository, IMediator mediator)
        {
            _interventionRepository = interventionRepository;
            _mediator = mediator;
            Title = "Interventi";
        }

        [ObservableProperty]
        bool _isRefreshing;

        [RelayCommand]
        async Task GoToAddIntervention()
        {
            await Shell.Current.GoToAsync(nameof(AddCustomerPage), true);
        }

        public ObservableRangeCollection<Intervention> Interventions { get; } = new();

        public async Task LoadInterventionsAsync()
        {
            var request = new GetInterventionsQuery();
            var response = await _mediator.Send(request, default);
            response.When(value =>
            {
                Interventions.AddRange(value);
            }, err =>
            {

            });
        }
    }
}
