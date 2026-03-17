namespace MobileVerticalHorizontalApp;

public partial class TripsTrapsTrull : ContentPage
{
    string[,] board = new string[3, 3];
    Button[,] buttons;

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

        buttons = new Button[,]
        {
            { A1, A2, A3 },
            { S1, S2, S3 },
            { D1, D2, D3 }
        };

        UpdateInfo();
        ResetGame();
        DisableBoard();
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

        UpdateInfo();
        ResetGame();
        DisableBoard();
        gameStarted = false;
        StatusLabel.Text = "Vajuta \"Alusta mängu\"";
    }

    private async Task AskWhoStarts()
    {
        while (true)
        {
            string result = await DisplayPromptAsync(
                "Kes alustab?",
                $"Kirjuta ainult 1 või 2\n1 - {player1Name}\n2 - {player2Name}",
                initialValue: "1");

            if (result == null)
            {
                player1Starts = true;
                break;
            }

            result = result.Trim();

            if (result == "1")
            {
                player1Starts = true;
                break;
            }

            if (result == "2")
            {
                player1Starts = false;
                break;
            }

            await DisplayAlertAsync("Viga", "Sisestada võib ainult 1 või 2.", "OK");
        }
    }

    private async void OnSymbolsClicked(object sender, EventArgs e)
    {
        string choice = await DisplayActionSheetAsync(
            "Vali sümbolite komplekt",
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

        UpdateInfo();
        ResetGame();
        DisableBoard();
        gameStarted = false;
        StatusLabel.Text = "Vajuta \"Alusta mängu\"";
    }

    private async void OnThemeClicked(object sender, EventArgs e)
    {
        string theme = await DisplayActionSheetAsync("Vali teema", "Loobu", null, "Hele", "Tume");

        if (theme == "Hele")
        {
            App.Current.UserAppTheme = AppTheme.Light;
            this.BackgroundColor = Color.FromArgb("#F3F4F6");
        }
        else if (theme == "Tume")
        {
            App.Current.UserAppTheme = AppTheme.Dark;
            this.BackgroundColor = Color.FromArgb("#111827");
        }
    }

    private async void OnRulesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Reegel());
    }

    private void OnStartGameClicked(object sender, EventArgs e)
    {
        ResetGame();
        EnableBoard();
        gameStarted = true;
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
            await DisplayAlertAsync("Viga", "Kõigepealt vajuta nuppu \"Alusta mängu\".", "OK");
            return;
        }

        Button button = (Button)sender;
        string[] pos = button.CommandParameter.ToString().Split(',');

        int row = int.Parse(pos[0]);
        int col = int.Parse(pos[1]);

        if (!string.IsNullOrEmpty(board[row, col]))
            return;

        string currentSymbol;
        string currentPlayerName;

        if (player1Turn)
        {
            currentSymbol = player1Symbol;
            currentPlayerName = player1Name;
            button.TextColor = Colors.Blue;
        }
        else
        {
            currentSymbol = player2Symbol;
            currentPlayerName = player2Name;
            button.TextColor = Colors.Red;
        }

        board[row, col] = currentSymbol;
        button.Text = currentSymbol;
        button.IsEnabled = false;

        if (CheckWin(currentSymbol))
        {
            gameStarted = false;

            bool again = await DisplayAlertAsync(
                "Võit",
                $"{currentPlayerName} võitis!\nSümbol: {currentSymbol}\nKas soovid veel mängida?",
                "Jah",
                "Ei");

            if (again)
            {
                ResetGame();
                EnableBoard();
                gameStarted = true;
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
                ResetGame();
                EnableBoard();
                gameStarted = true;
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
        board = new string[3, 3];
        player1Turn = player1Starts;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buttons[i, j].Text = "";
                buttons[i, j].IsEnabled = true;
                buttons[i, j].TextColor = Colors.Black;
                buttons[i, j].BackgroundColor = Color.FromArgb("#E0E7FF");
            }
        }

        UpdateInfo();
        UpdateStatus();
    }

    private void DisableBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                buttons[i, j].IsEnabled = false;
            }
        }
    }

    private void EnableBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (string.IsNullOrEmpty(board[i, j]))
                    buttons[i, j].IsEnabled = true;
            }
        }
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
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                return true;

            if (board[0, i] == symbol && board[1, i] == symbol && board[2, i] == symbol)
                return true;
        }

        if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
            return true;

        if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
            return true;

        return false;
    }

    private bool CheckDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (string.IsNullOrEmpty(board[i, j]))
                    return false;
            }
        }

        return true;
    }
}