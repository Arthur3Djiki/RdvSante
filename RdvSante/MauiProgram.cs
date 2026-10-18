using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace RdvSante;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Champs de saisie sans soulignement natif (le cadre est dessiné par le style "Field").
        EntryHandler.Mapper.AppendToMapping("SansSoulignement", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
