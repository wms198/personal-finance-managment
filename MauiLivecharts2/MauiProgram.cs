﻿using Microsoft.Extensions.Logging;

namespace MauiLivecharts2;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting; 

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
     
        var builder = MauiApp.CreateBuilder();
        builder
            .UseSkiaSharp(true) 
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}