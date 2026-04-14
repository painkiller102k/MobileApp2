namespace MobileVerticalHorizontalApp;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();

var pages = new List<(string, ContentPage)>
{
    ("Tekst", new TextPage(1)),
    ("Kujund", new FigurePage()), 
    ("Valgusfoor", new MainPage()),
    ("Aeg ja kuupäev", new DateTimePage()),
    ("SliderPage", new StepperSliderPage()),
    ("RGBPage", new RGBPage()),
    ("Lumememm", new Lumememm()),
    ("PopUp", new PopUp()),
    ("TripsTrapsTrull", new TripsTrapsTrull()),
    ("Kontaktandmed", new Kontaktandmed()),
    ("ListPage", new ListViewPage())
};


        VerticalStackLayout vst = new VerticalStackLayout { Padding = 20, Spacing = 15 };

        foreach (var (name, page) in pages)
        {
            Button nupp = new Button
            {
                Text = name,
                FontSize = 30,
                BackgroundColor = Colors.WhiteSmoke,
                TextColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 50
            };

            nupp.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(page);
            };

            vst.Add(nupp);
        }

        Content = new ScrollView { Content = vst };
    }
}
