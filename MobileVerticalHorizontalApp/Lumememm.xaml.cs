using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MobileVerticalHorizontalApp;

public partial class Lumememm : ContentPage
{
    Random rnd = new Random();

    public Lumememm()
    {
        InitializeComponent();

        ActionPicker.SelectedIndex = 0; // esimene peida
        SpeedLabel.Text = ((int)SpeedStepper.Value).ToString(); 

        ApplyOpacity(OpacitySlider.Value);
    }

    void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        ApplyOpacity(e.NewValue);
    }

    void ApplyOpacity(double value)
    {
        Bucket.Opacity = value;
        Head.Opacity = value;
        Body.Opacity = value;

        Eye1.Opacity = value;
        Eye2.Opacity = value;
        Dot1.Opacity = value;
        Dot2.Opacity = value;
        Dot3.Opacity = value;

        HatBrim.Opacity = value;
        HatBand.Opacity = value;
        HatBuckle.Opacity = value;
        HatShine.Opacity = value;
    }

    void OnSpeedChanged(object sender, ValueChangedEventArgs e)
    {
        SpeedLabel.Text = ((int)e.NewValue).ToString();
    }

    async void OnRunClicked(object sender, EventArgs e)
    {
        string action = ActionPicker.SelectedItem as string;
        ActionLabel.Text = $"Tegevus: {action}";

        uint speed = (uint)(SpeedStepper.Maximum + SpeedStepper.Minimum - SpeedStepper.Value);

        if (action == "Peida lumememm")
        {
            SnowmanArea.IsVisible = false;
        }
        else if (action == "Näita lumememm")
        {
            RestoreSnowman();
        }
        else if (action == "Muuda värvi")
        {
            RestoreSnowman();

            bool ok = await DisplayAlertAsync("Kinnitus", "Muuta värvi?", "Jah", "Ei");
            if (ok)
            {
                var c = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                Head.BackgroundColor = c;
                Body.BackgroundColor = c;
            }
        }
        else if (action == "Sulata")
        {
            RestoreSnowman();

            await Task.WhenAll( // teha mitu ulesannet 
                SnowmanArea.FadeToAsync(0, speed),
                SnowmanArea.ScaleToAsync(0.2, speed)
            );

            SnowmanArea.IsVisible = false;
        }
        else if (action == "Tantsi")
        {
            RestoreSnowman();

            await SnowmanArea.TranslateToAsync(-50, 0, speed);
            await SnowmanArea.TranslateToAsync(50, 0, speed);
            await SnowmanArea.TranslateToAsync(0, 0, speed);
        }
    }

    void RestoreSnowman()
    {
        SnowmanArea.IsVisible = true;

        SnowmanArea.Opacity = 1;
        SnowmanArea.Scale = 1;
        SnowmanArea.TranslationX = 0;
        SnowmanArea.TranslationY = 0;

        ApplyOpacity(OpacitySlider.Value);
    }
}