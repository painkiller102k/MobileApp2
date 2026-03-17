namespace MobileVerticalHorizontalApp;

public partial class Reegel : ContentPage
{
    public Reegel()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}