using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimtekData.Repository.Abstractions;
using SimtekMaui.Application;
using SimtekMaui.Application.Infrastructure;
using SimtekMaui.Data;
using SimtekMaui.Data.Repositories;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.ViewModels;
using SimtekMaui.Views;

namespace SimtekMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("FontAwesome.otf", "FontAwesome");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .AddPages();
        builder.AddServices();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(MediatorHook)
                .Assembly);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services.AddScoped<MainViewModel>();
        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<AddInterventionPage>();
        builder.Services.AddScoped<AddInterventionViewModel>();
        builder.Services.AddScoped<AddCustomerPage>();
        builder.Services.AddScoped<AddCustomerViewModel>();
        return builder;
    }

    public static void AddServices(this MauiAppBuilder builder)
    {

        builder.Services.AddScoped<ISimtekService, SimtekService>();
        builder.Services.AddScoped<IInterventionRepository,FakeInterventionRepository>();
        builder.Services.AddScoped<ICustomerRepository, FakeCustomerRepository>();
    }
}