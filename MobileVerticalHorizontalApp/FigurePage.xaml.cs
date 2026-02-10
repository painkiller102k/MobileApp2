using Microsoft.Maui.Controls.Shapes;

namespace MobileVerticalHorizontalApp;

public partial class FigurePage : ContentPage
{
    BoxView boxView;
    Ellipse pall;
    Polygon kolmnurk;
    Random rnd = new Random();
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    VerticalStackLayout vsl;

    public FigurePage()
    {
        int r = rnd.Next(256);
        int g = rnd.Next(256);
        int b = rnd.Next(256);

        boxView = new BoxView
        {
            Color = Color.FromRgb(r, g, b),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            CornerRadius = 30
        };

        TapGestureRecognizer tapBox = new TapGestureRecognizer();
        boxView.GestureRecognizers.Add(tapBox);
        tapBox.Tapped += (sender, e) =>
        {
            int r1 = rnd.Next(256);
            int g1 = rnd.Next(256);
            int b1 = rnd.Next(256);
            boxView.Color = Color.FromRgb(r1, g1, b1);
            boxView.WidthRequest += 20;
            boxView.HeightRequest += 30;

            if (boxView.WidthRequest > DeviceDisplay.MainDisplayInfo.Width / 3)
            {
                boxView.WidthRequest = 200;
                boxView.HeightRequest = 200;
            }
        };

        pall = new Ellipse
        {
            WidthRequest = 200,
            HeightRequest = 200,
            Fill = new SolidColorBrush(Color.FromRgb(g, r, b)),
            Stroke = Colors.Blue,
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        TapGestureRecognizer tapPall = new TapGestureRecognizer();
        pall.GestureRecognizers.Add(tapPall);
        tapPall.Tapped += (sender, e) =>
        {
            int r1 = rnd.Next(256);
            int g1 = rnd.Next(256);
            int b1 = rnd.Next(256);
            pall.Fill = new SolidColorBrush(Color.FromRgb(r1, g1, b1));
            pall.WidthRequest += 20;
            pall.HeightRequest += 20;

            if (pall.WidthRequest > DeviceDisplay.MainDisplayInfo.Width / 3)
            {
                pall.WidthRequest = 200;
                pall.HeightRequest = 200;
            }
        };

        kolmnurk = new Polygon
        {
            Points = new PointCollection
            {
                new Point(0,200),
                new Point(100,0),
                new Point(200,200),
            },
            Fill = new SolidColorBrush(Color.FromRgb(g,r,b)),
            Stroke = Colors.AliceBlue,
            StrokeThickness = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tapKolmnurk = new TapGestureRecognizer();
        kolmnurk.GestureRecognizers.Add(tapKolmnurk);

        tapKolmnurk.Tapped += async (sender, e) =>
        {
            int r1 = rnd.Next(256);
            int g1 = rnd.Next(256);
            int b1 = rnd.Next(256);
            kolmnurk.Fill = new SolidColorBrush(Color.FromRgb(r1, g1, b1));

            kolmnurk.Scale += 0.1;
            if (kolmnurk.Scale > 2) 
                kolmnurk.Scale = 1;

            await kolmnurk.RotateTo(kolmnurk.Rotation + 45, 300);
        };




        // nupud
        hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        for (int i = 0; i < nupud.Count; i++)
        {
            Button nupp = new Button
            {
                Text = nupud[i],
                FontSize = 20,
                TextColor = Colors.White,
                BackgroundColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 40,
                ZIndex = i
            };

            nupp.Clicked += Liikumine;
            hsl.Add(nupp);
        }

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 20,
            Children = { boxView, pall, kolmnurk, hsl },
            HorizontalOptions = LayoutOptions.Center
        };

        Content = vsl;
    }

    private async void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;

        if (nupp.ZIndex == 0)          // Tagasi
            await Navigation.PopAsync();
        else if (nupp.ZIndex == 1)     // Avaleht
            await Navigation.PopToRootAsync();
        else if (nupp.ZIndex == 2)     // Edasi
            await Navigation.PushAsync(new StartPage());
    }
}
