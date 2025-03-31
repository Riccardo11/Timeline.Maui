using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Timeline.Maui.Samples.Main.Pages;
using Timeline.Maui.Samples.Main.ViewModels;

namespace Timeline.Maui.Samples.Main;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransientWithShellRoute<FirstPage, BaseViewModel>(nameof(FirstPage));

		return builder.Build();
	}
}
