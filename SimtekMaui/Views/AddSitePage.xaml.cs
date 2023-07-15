using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimtekMaui.ViewModels;

namespace SimtekMaui.Views;

public partial class AddSitePage : ContentPage
{
    public AddSitePage(AddSiteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}