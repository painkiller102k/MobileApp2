using System.Collections.Generic;

namespace MobileVerticalHorizontalApp;

public partial class PopUp : ContentPage
{
    Random random = new Random();

    string userName = "";
    string selectedTopic = "";

    Dictionary<string, string> riddleQuestions = new Dictionary<string, string>()
    {
        { "Mis on see: tal on neli jalga, aga ta ei kõnni?", "laud" },
        { "Mis on see: tal on hambad, aga ta ei söö?", "kamm" },
        { "Mis on see: valge väljast, kollane seest?", "muna" }
    };

    Dictionary<string, string> mathQuestions = new Dictionary<string, string>()
    {
        { "Kui palju on 2 + 3 ?", "5" },
        { "Kui palju on 4 x 2 ?", "8" },
        { "Kui palju on 7 + 1 ?", "8" },
        { "Kui palju on 3 x 3 ?", "9" },
        { "Kui palju on 6 x 6 ?", "36"},
        { "Kui palju on 7 x 5 ?", "35"},
    };

    public PopUp()
    {
        InitializeComponent();
    }

    private async void OnTopicClicked(object sender, EventArgs e)
    {
        string topic = await DisplayActionSheetAsync(
            "Vali teema",
            "Loobu",
            null,
            "Tavalised mõistatused",
            "Matemaatilised näited"
        );

        if (topic == "Loobu" || topic == null)
        {
            return;
        }

        selectedTopic = topic;
        InfoLabel.Text = "Valitud teema: " + selectedTopic;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        if (selectedTopic == "")
        {
            await DisplayAlertAsync("Viga", "Palun vali kõigepealt teema.", "OK");
            return;
        }

        if (userName == "")
        {
            string name = await DisplayPromptAsync("Tere!", "Mis sinu nimi on?");

            if (name == null || name == "")
            {
                await DisplayAlertAsync("Viga", "Sa ei sisestanud nime.", "OK");
                return;
            }

            userName = name;
        }

        InfoLabel.Text = "Tere, " + userName + "! Teema: " + selectedTopic;

        await AskQuestion();
    }

    private async Task AskQuestion()
    {
        string question = "";
        string correctAnswer = "";

        if (selectedTopic == "Tavalised mõistatused")
        {
            List<string> questions = new List<string>(riddleQuestions.Keys); // kõik keys - spisok
            int index = random.Next(questions.Count);       

            question = questions[index];
            correctAnswer = riddleQuestions[question];
        }
        else if (selectedTopic == "Matemaatilised näited")
        {
            List<string> questions = new List<string>(mathQuestions.Keys); // kõik keys - spisok
            int index = random.Next(questions.Count);

            question = questions[index];
            correctAnswer = mathQuestions[question];
        }

        string answer = await DisplayPromptAsync("Küsimus", userName + ", " + question);

        if (answer == null || answer == "")
        {
            await DisplayAlertAsync("Viga", "Vastus jäi sisestamata.", "OK");
        }
        else
        {
            if (selectedTopic == "Tavalised mõistatused")
            {
                if (answer.ToLower().Trim() == correctAnswer)
                {
                    InfoLabel.Text = "Tubli, " + userName + "! Vastus on õige.";
                    await DisplayAlertAsync("Tulemus", "Õige vastus!", "OK");
                }
                else
                {
                    InfoLabel.Text = "Vale vastus. Õige vastus on " + correctAnswer;
                    await DisplayAlertAsync("Tulemus", "Vale vastus. Õige vastus on " + correctAnswer, "OK");
                }
            }
            else
            {
                if (answer.Trim() == correctAnswer)
                {
                    InfoLabel.Text = "Tubli, " + userName + "! Vastus on õige.";
                    await DisplayAlertAsync("Tulemus", "Õige vastus!", "OK");
                }
                else
                {
                    InfoLabel.Text = "Vale vastus. Õige vastus on " + correctAnswer;
                    await DisplayAlertAsync("Tulemus", "Vale vastus. Õige vastus on " + correctAnswer, "OK");
                }
            }
        }

        bool continueGame = await DisplayAlertAsync(
            "Küsimus",
            userName + ", kas soovid jätkata?",
            "Jah",
            "Ei"
        );

        if (continueGame)
        {
            await AskQuestion();
        }
        else
        {
            InfoLabel.Text = "Aitäh mängimast, " + userName + "!";
        }
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        string color = await DisplayActionSheetAsync(
            "Vali värv",
            "Loobu",
            null,
            "Valge",
            "Kollane",
            "Sinine",
            "Roosa"
        );

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