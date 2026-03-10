namespace MobileVerticalHorizontalApp;

public partial class PopUp : ContentPage
{
    Random random = new Random();

    public PopUp()
    {
        InitializeComponent();
    }

    private async void OnNameClicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Tere!", "Mis sinu nimi on?");

        if (name == null || name == "")
        {
            await DisplayAlert("Viga", "Sa ei sisestanud nime.", "OK");
        }
        else
        {
            InfoLabel.Text = "Tere, " + name + "!";
            await DisplayAlert("Tervitus", "Tere, " + name + "!", "OK");
        }
    }

    private async void OnMathClicked(object sender, EventArgs e)
    {
        int a = random.Next(1, 10);
        int b = random.Next(1, 10);

        string answer = await DisplayPromptAsync("Test", "Kui palju on " + a + " x " + b + " ?");

        if (answer == null || answer == "")
        {
            await DisplayAlert("Viga", "Vastus jäi sisestamata.", "OK");

            bool continueGame1 = await DisplayAlert("Küsimus", "Kas soovid jätkata?", "Jah", "Ei");

            if (continueGame1)
            {
                OnMathClicked(sender, e);
            }

            return;
        }

        int userNumber;

        if (int.TryParse(answer, out userNumber))
        {
            if (userNumber == a * b)
            {
                InfoLabel.Text = "Tubli! Vastus on õige.";
                await DisplayAlert("Tulemus", "Õige vastus!", "OK");
            }
            else
            {
                InfoLabel.Text = "Vale vastus. Õige vastus on " + (a * b);
                await DisplayAlert("Tulemus", "Vale vastus. Õige vastus on " + (a * b), "OK");
            }
        }
        else
        {
            await DisplayAlert("Viga", "Sisesta number.", "OK");

            bool continueGame2 = await DisplayAlert("Küsimus", "Kas soovid jätkata?", "Jah", "Ei");

            if (continueGame2)
            {
                OnMathClicked(sender, e);
            }

            return;
        }

        bool continueGame3 = await DisplayAlert("Küsimus", "Kas soovid jätkata?", "Jah", "Ei");

        if (continueGame3)
        {
            OnMathClicked(sender, e);
        }
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        string color = await DisplayActionSheet("Vali värv", "Loobu", null,
            "Valge", "Kollane", "Sinine", "Roosa");

        if (color == "Valge")
        {
            BackgroundColor = Colors.White;
            InfoLabel.Text = "Valitud on valge värv";
        }
        else if (color == "Kollane")
        {
            BackgroundColor = Colors.LightYellow;
            InfoLabel.Text = "Valitud on kollane värv";
        }
        else if (color == "Sinine")
        {
            BackgroundColor = Colors.LightBlue;
            InfoLabel.Text = "Valitud on sinine värv";
        }
        else if (color == "Roosa")
        {
            BackgroundColor = Colors.LightPink;
            InfoLabel.Text = "Valitud on roosa värv";
        }
    }
}