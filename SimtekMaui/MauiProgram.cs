using CommunityToolkit.Maui;
using MediatR;
using Microsoft.Extensions.Logging;
using SimtekMaui.Application;
using SimtekMaui.Application.Infrastructure;
using SimtekMaui.Application.Repositories;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Utils;
using SimtekMaui.ViewModels;
using SimtekMaui.Views;
using The49.Maui.BottomSheet;

namespace SimtekMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseBottomSheet()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("FontAwesome.otf", "FontAwesome");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .AddPages();
        builder.AddServices();
        builder.Services.AddMediatR(typeof(MediatorHook));
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AddInterventionPage>();
        builder.Services.AddTransient<AddInterventionViewModel>();
        builder.Services.AddTransient<AddCustomerPage>();
        builder.Services.AddTransient<AddCustomerViewModel>();
        builder.Services.AddTransient<AddSitePage>();
        builder.Services.AddTransient<AddSiteViewModel>();
        return builder;
    }

    private static void AddServices(this MauiAppBuilder builder)
    {

        builder.Services.AddTransient<ISimtekService, SimtekService>();
        builder.Services.AddTransient<IInterventionRepository,FakeInterventionRepository>();
        builder.Services.AddTransient<ISiteRepository, FakeSiteRepository>();
        builder.Services.AddTransient<ICustomerRepository, FakeCustomerRepository>();
        builder.Services.AddSingleton<NewInterventionStateBuilder>();
    }
}