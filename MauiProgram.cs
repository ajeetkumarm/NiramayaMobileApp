using Kadam.Services;
using Kadam.ViewModels;
using Kadam.Views;
using Microsoft.Extensions.Logging;

namespace Kadam
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                ;
            // Configure the Entry handler for Android
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                // Remove the underline by setting the BackgroundTintList to transparent
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(DatePicker), (handler, view) =>
            {
#if ANDROID
                // Remove the underline by setting the BackgroundTintList to transparent
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
            {
#if ANDROID
                // Remove the underline by setting the BackgroundTintList to transparent
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            

            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IToolbarService, ToolbarService>();
            // Register ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<StudentRegistrationViewModel>();

            // Register Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<Dashboard>();
            builder.Services.AddTransient<StudentRegistration>();
            //builder.Services.AddTransient<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
