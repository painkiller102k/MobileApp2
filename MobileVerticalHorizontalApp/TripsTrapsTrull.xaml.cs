namespace MobileVerticalHorizontalApp;

public partial class TripsTrapsTrull : ContentPage
{
    string player1Name = "Mängija 1";
    string player2Name = "Mängija 2";

    string player1Symbol = "X";
    string player2Symbol = "O";

    bool player1Starts = true;
    bool player1Turn = true;
    bool gameStarted = false;

    public TripsTrapsTrull()
    {
        InitializeComponent();
        ResetGame();
        DisableBoard();
        UpdateInfo();
    }

    private async void OnPlayersClicked(object sender, EventArgs e)
    {
        string name1 = await DisplayPromptAsync("Mängija 1", "Sisesta esimese mängija nimi:", initialValue: player1Name);
        if (!string.IsNullOrWhiteSpace(name1))
            player1Name = name1;

        string name2 = await DisplayPromptAsync("Mängija 2", "Sisesta teise mängija nimi:", initialValue: player2Name);
        if (!string.IsNullOrWhiteSpace(name2))
            player2Name = name2;

        await AskWhoStarts();

        gameStarted = false;
        ResetGame();
        DisableBoard();
        UpdateInfo();
        StatusLabel.Text = "Vajuta \"Alusta mängu\"";
    }

    private async Task AskWhoStarts()
    {
        while (true)
        {
            string answer = await DisplayPromptAsync(
                "Kes alustab?",
                $"Sisesta ainult 1 või 2\n1 - {player1Name}\n2 - {player2Name}",
                initialValue: "1");

            if (answer == null || answer.Trim() == "1")
            {
                player1Starts = true;
                return;
            }

            if (answer.Trim() == "2")
            {
                player1Starts = false;
                return;
            }

            await DisplayAlertAsync("Viga", "Sisestada võib ainult 1 või 2.", "OK");
        }
    }

    private async void OnSymbolsClicked(object sender, EventArgs e)
    {
        string choice = await DisplayActionSheetAsync(
            "Vali sümbolid",
            "Loobu",
            null,
            "X ja O",
            "🙂 ja 😎");

        if (choice == "X ja O")
        {
            player1Symbol = "X";
            player2Symbol = "O";
        }
        else if (choice == "🙂 ja 😎")
        {
            player1Symbol = "🙂";
            player2Symbol = "😎";
        }

        gameStarted = false;
        ResetGame();
        DisableBoard();
        UpdateInfo();
        StatusLabel.Text = "Vajuta \"Alusta mängu\"";
    }

    private async void OnThemeClicked(object sender, EventArgs e)
    {
        string theme = await DisplayActionSheetAsync("Vali teema", "Loobu", null, "Hele", "Tume");

        if (theme == "Hele")
        {
            App.Current.UserAppTheme = AppTheme.Light;
            BackgroundColor = Color.FromArgb("#F3F4F6");
        }
        else if (theme == "Tume")
        {
            App.Current.UserAppTheme = AppTheme.Dark;
            BackgroundColor = Color.FromArgb("#111827");
        }
    }

    private async void OnRulesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Reegel());
    }

    private void OnStartGameClicked(object sender, EventArgs e)
    {
        gameStarted = true;
        ResetGame();
        EnableBoard();
        UpdateStatus();
    }

    private void OnNewGameClicked(object sender, EventArgs e)
    {
        ResetGame();

        if (gameStarted)
            EnableBoard();
        else
            DisableBoard();
    }

    private async void OnCellClicked(object sender, EventArgs e)
    {
        if (!gameStarted)
        {
            await DisplayAlertAsync("Viga", "Kõigepealt vajuta \"Alusta mängu\".", "OK");
            return;
        }

        Button button = (Button)sender; //objekt nupp

        if (!string.IsNullOrEmpty(button.Text)) //пустая ли клетка
            return;

        if (player1Turn)
        {
            button.Text = player1Symbol;
            button.TextColor = Colors.Blue;
        }
        else
        {
            button.Text = player2Symbol;
            button.TextColor = Colors.Red;
        }

        button.IsEnabled = false;

        string currentSymbol = player1Turn ? player1Symbol : player2Symbol; // igrok symbol
        string currentName = player1Turn ? player1Name : player2Name; // igrok name

        if (CheckWin(currentSymbol))
        {
            gameStarted = false;

            bool again = await DisplayAlertAsync(
                "Võit",
                $"{currentName} võitis!\nKas soovid veel mängida?",
                "Jah",
                "Ei");

            if (again)
            {
                gameStarted = true;
                ResetGame();
                EnableBoard();
            }
            else
            {
                DisableBoard();
                StatusLabel.Text = "Mäng on lõppenud";
            }

            return;
        }

        if (CheckDraw())
        {
            gameStarted = false;

            bool again = await DisplayAlertAsync(
                "Viik",
                "Mäng lõppes viigiga! Kas soovid veel mängida?",
                "Jah",
                "Ei");

            if (again)
            {
                gameStarted = true;
                ResetGame();
                EnableBoard();
            }
            else
            {
                DisableBoard();
                StatusLabel.Text = "Mäng on lõppenud";
            }

            return;
        }

        player1Turn = !player1Turn;
        UpdateStatus();
    }

    private void ResetGame()
    {
        player1Turn = player1Starts;

        ClearButton(A1);
        ClearButton(A2);
        ClearButton(A3);
        ClearButton(S1);
        ClearButton(S2);
        ClearButton(S3);
        ClearButton(D1);
        ClearButton(D2);
        ClearButton(D3);

        UpdateStatus();
        UpdateInfo();
    }

    private void ClearButton(Button button)
    {
        button.Text = "";
        button.TextColor = Colors.Black;
        button.BackgroundColor = Color.FromArgb("#E0E7FF");
        button.IsEnabled = true;
    }

    private void DisableBoard()
    {
        A1.IsEnabled = false;
        A2.IsEnabled = false;
        A3.IsEnabled = false;
        S1.IsEnabled = false;
        S2.IsEnabled = false;
        S3.IsEnabled = false;
        D1.IsEnabled = false;
        D2.IsEnabled = false;
        D3.IsEnabled = false;
    }

    private void EnableBoard()
    {
        A1.IsEnabled = string.IsNullOrEmpty(A1.Text);
        A2.IsEnabled = string.IsNullOrEmpty(A2.Text);
        A3.IsEnabled = string.IsNullOrEmpty(A3.Text);
        S1.IsEnabled = string.IsNullOrEmpty(S1.Text);
        S2.IsEnabled = string.IsNullOrEmpty(S2.Text);
        S3.IsEnabled = string.IsNullOrEmpty(S3.Text);
        D1.IsEnabled = string.IsNullOrEmpty(D1.Text);
        D2.IsEnabled = string.IsNullOrEmpty(D2.Text);
        D3.IsEnabled = string.IsNullOrEmpty(D3.Text);
    }

    private void UpdateInfo()
    {
        PlayersLabel.Text = $"{player1Name} ({player1Symbol}) vs {player2Name} ({player2Symbol})";
    }

    private void UpdateStatus()
    {
        if (!gameStarted)
        {
            StatusLabel.Text = "Vajuta \"Alusta mängu\"";
            return;
        }

        if (player1Turn)
            StatusLabel.Text = $"Käik: {player1Name} ({player1Symbol})";
        else
            StatusLabel.Text = $"Käik: {player2Name} ({player2Symbol})";
    }

    private bool CheckWin(string symbol)
    {
        if (A1.Text == symbol && A2.Text == symbol && A3.Text == symbol) return true;
        if (S1.Text == symbol && S2.Text == symbol && S3.Text == symbol) return true;
        if (D1.Text == symbol && D2.Text == symbol && D3.Text == symbol) return true;

        if (A1.Text == symbol && S1.Text == symbol && D1.Text == symbol) return true;
        if (A2.Text == symbol && S2.Text == symbol && D2.Text == symbol) return true;
        if (A3.Text == symbol && S3.Text == symbol && D3.Text == symbol) return true;

        if (A1.Text == symbol && S2.Text == symbol && D3.Text == symbol) return true;
        if (A3.Text == symbol && S2.Text == symbol && D1.Text == symbol) return true;

        return false;
    }

    private bool CheckDraw() // text - true - draw 
    {
        return !string.IsNullOrEmpty(A1.Text) &&
               !string.IsNullOrEmpty(A2.Text) &&
               !string.IsNullOrEmpty(A3.Text) &&
               !string.IsNullOrEmpty(S1.Text) &&
               !string.IsNullOrEmpty(S2.Text) &&
               !string.IsNullOrEmpty(S3.Text) &&
               !string.IsNullOrEmpty(D1.Text) &&
               !string.IsNullOrEmpty(D2.Text) &&
               !string.IsNullOrEmpty(D3.Text);
    }
}