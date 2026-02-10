using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileVerticalHorizontalApp;

public partial class TextPage : ContentPage
{
    List<string> nupud = new() { "Tagasi", "Avaleht", "Edasi" };

    public TextPage(int i)
    {
        InitializeComponent();

        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 20,
                TextColor = Colors.White,
                BackgroundColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 40,
                ZIndex = j
            };

            nupp.Clicked += Liikumine;
            hsl.Add(nupp);
        }

        editor.TextChanged += (sender, e) =>
        {
            lbl.Text = editor.Text;
        };
    }

    private async void Btn_Clicked(object? sender, EventArgs e)
    {
        var locales = await TextToSpeech.GetLocalesAsync();

        SpeechOptions options = new SpeechOptions
        {
            Volume = 0.75f,   // громкость
            Pitch = 1.5f,     // высота голоса
            Locale = locales.FirstOrDefault()
        };

        string text = editor.Text;

        if (string.IsNullOrWhiteSpace(text))
        {
            await DisplayAlert("Viga", "Palun sisesta tekst", "OK");
            return;
        }

        await TextToSpeech.SpeakAsync(text, options);
    }

    private async void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;

        if (nupp.ZIndex == 0)          // Tagasi
            await Navigation.PopAsync();
        else if (nupp.ZIndex == 1)     // Avaleht
            await Navigation.PopToRootAsync();
        else if (nupp.ZIndex == 2)     // Edasi
            await Navigation.PushAsync(new FigurePage());
    }
}
