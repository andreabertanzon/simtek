using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimtekMaui.Application;
using SimtekMaui.Data.Repositories;
using SimtekMaui.ViewModels;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddScoped<MainViewModel>();
        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<InterventionRepository>();
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
}