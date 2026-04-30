namespace MobileVerticalHorizontalApp;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();

        Title = "Minu rakendus";

        var pages = new List<(string, ContentPage)>
        {
            ("Tekst", new TextPage(1)),
            ("Kujund", new FigurePage()),
            ("Valgusfoor", new Valgusfoor()),
            ("Aeg ja kuupäev", new DateTimePage()),
            ("SliderPage", new StepperSliderPage()),
            ("RGBPage", new RGBPage()),
            ("Lumememm", new Lumememm()),
            ("PopUp", new PopUp()),
            ("TripsTrapsTrull", new TripsTrapsTrull()),
            ("Kontaktandmed", new Kontaktandmed()),
            ("ListPage", new ListViewPage()),
            ("CarouselView", new CarouselView()),
            ("Pilt MAUI", new MainPage())
        };

        var layout = new VerticalStackLayout
        {
            Padding = new Thickness(20, 40, 20, 20),
            Spacing = 15
        };

        layout.Add(new Label
        {
            Text = "📱 Rakendused",
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        });

        layout.Add(new Label
        {
            Text = "Vali leht",
            FontSize = 16,
            TextColor = Colors.Gray
        });

        foreach (var (name, page) in pages)
        {
            var frame = new Frame
            {
                CornerRadius = 15,
                Padding = 0,
                HasShadow = true,
                BackgroundColor = Colors.White
            };

            var btn = new Button
            {
                Text = name,
                FontSize = 20,
                BackgroundColor = Colors.Transparent,
                TextColor = Colors.Black,
                HeightRequest = 60
            };

            btn.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(page);
            };

            frame.Content = btn;

            layout.Add(frame);
        }

        Content = new ScrollView
        {
            Content = layout,
            BackgroundColor = Color.FromRgb(245, 245, 245)
        };
    }
}