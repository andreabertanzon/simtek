using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Data.Repositories;
using SimtekMaui.Utils;

namespace SimtekMaui.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly InterventionRepository _interventionRepository;

        public MainViewModel(InterventionRepository interventionRepository)
        {
            _interventionRepository = interventionRepository;
            Title = "Home";
        }

        [ObservableProperty]
        bool _isRefreshing;

        public ObservableRangeCollection<InterventionDto> Interventions { get; } = new();

        public async Task LoadInterventionsAsync()
        {
            var interventionDtos = await _interventionRepository.GetAsync();
            Interventions.AddRange(interventionDtos);
        }
    }
}
