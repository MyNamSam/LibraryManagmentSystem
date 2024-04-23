using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using LibraryManagmentSystem.DataHelper;
using LibraryManagmentSystem.ViewModels;
using LibraryManagmentSystem.Views;
using Syncfusion.Maui.Core.Hosting;

namespace LibraryManagmentSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<IBookHelper, BookHelper>();
            builder.Services.AddTransient<AddOrUpdateBookPageViewModel>();
            builder.Services.AddTransient<AddOrUpdateBookPage>();

            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<HomePage>();

            builder.Services.AddTransient<DetailsPageViewModel>();
            builder.Services.AddTransient<DetailsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
