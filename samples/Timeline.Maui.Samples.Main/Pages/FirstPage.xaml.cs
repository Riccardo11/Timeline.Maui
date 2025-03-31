using LoremNET;

namespace Timeline.Maui.Samples.Main.Pages;

public partial class FirstPage : ContentPage
{
    public FirstPage()
    {
        InitializeComponent();
        Timeline.ItemsSource = Enumerable
            .Range(0, 10)
            .Select(_ => Lorem.Words(3));
    }
}