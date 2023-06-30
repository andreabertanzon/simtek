using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SimtekDomain;
using SimtekDomain.InterventionCQRS;
using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Data.Repositories;
using SimtekMaui.Utils;
using CommunityToolkit.Mvvm.Input;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Views;

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
            await Shell.Current.GoToAsync(nameof(AddCustomerPage),true);
        }

        public ObservableRangeCollection<Intervention> Interventions { get; } = new();

        public async Task LoadInterventionsAsync()
        {
            var request = new GetInterventionsQuery();
            var response = await _mediator.Send(request, default);
            response.Switch(value =>
            {
                Interventions.AddRange(value);
            }, err =>
            {

            });
        }
    }
}
