using Timeline.Maui.Samples.Main.Pages;

namespace Timeline.Maui.Samples.Main;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(FirstPage));
	}
}
