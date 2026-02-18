namespace MobileVerticalHorizontalApp;

public partial class MainPage : ContentPage
{
    BoxView redLight;
    BoxView yellowLight;
    BoxView greenLight;

    Label pealkiri;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;

    bool foorOn = false; // включен ли фоор

    public MainPage()
    {
        // Pealkiri
        pealkiri = new Label
        {
            Text = "Vali valgus",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center
        };

        redLight = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            CornerRadius = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tapRed = new TapGestureRecognizer(); //
        redLight.GestureRecognizers.Add(tapRed); //
        tapRed.Tapped += (s, e) =>
        {
            if (foorOn)
                pealkiri.Text = "Seisa";
        };

        yellowLight = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            CornerRadius = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tapYellow = new TapGestureRecognizer();
        yellowLight.GestureRecognizers.Add(tapYellow);
        tapYellow.Tapped += (s, e) =>
        {
            if (foorOn)
                pealkiri.Text = "Valmista";
        };

        greenLight = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            CornerRadius = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tapGreen = new TapGestureRecognizer();
        greenLight.GestureRecognizers.Add(tapGreen);
        tapGreen.Tapped += (s, e) =>
        {
            if (foorOn)
                pealkiri.Text = "Sõida";
        };

        hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        Button sisse = new Button
        {
            Text = "Sisse",
            FontSize = 20,
            TextColor = Colors.White,
            BackgroundColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 40,
            ZIndex = 0
        };
        sisse.Clicked += LulitaSisse;

        Button valja = new Button
        {
            Text = "Välja",
            FontSize = 20,
            TextColor = Colors.White,
            BackgroundColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 40,
            ZIndex = 1
        };
        valja.Clicked += LulitaValja;

        hsl.Add(sisse);
        hsl.Add(valja);

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children = { pealkiri, redLight, yellowLight, greenLight, hsl }
        };

        Content = vsl;
    }

    private void LulitaSisse(object? sender, EventArgs e)
    {
        foorOn = true;
        pealkiri.Text = "Vali valgus";

        redLight.Color = Colors.Red;
        yellowLight.Color = Colors.Yellow;
        greenLight.Color = Colors.Green;
    }

    private void LulitaValja(object? sender, EventArgs e)
    {
        foorOn = false;
        pealkiri.Text = "Lülita esmalt foor sisse";

        redLight.Color = Colors.Gray;
        yellowLight.Color = Colors.Gray;
        greenLight.Color = Colors.Gray;
    }
}