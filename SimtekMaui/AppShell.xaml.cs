using SimtekMaui.Views;

namespace SimtekMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddInterventionPage), typeof(AddInterventionPage));
        Routing.RegisterRoute(nameof(AddCustomerPage),typeof(AddCustomerPage));
        Routing.RegisterRoute(nameof(AddSitePage),typeof(AddSitePage));
    }
}