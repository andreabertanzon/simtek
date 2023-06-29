using SimtekMaui.Views;

namespace SimtekMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddInterventionPage), typeof(AddInterventionPage));
    }
}