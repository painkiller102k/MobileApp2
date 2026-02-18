using Microsoft.Maui.Layouts;
using System.Runtime.CompilerServices;

namespace MobileVerticalHorizontalApp;

public partial class StepperSliderPage : ContentPage
{
    Label label;
    Stepper stepper;
    Slider slider;
    AbsoluteLayout al;
    public StepperSliderPage()
    {
        label = new Label
        {
            Text = "...",
            BackgroundColor = Colors.Black,
        };
        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 100,
            Increment = 5,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += Stepper_Slider_ValueChanged;
        slider = new Slider
        {
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.SkyBlue,
            MaximumTrackColor = Colors.DarkBlue,
            ThumbColor = Colors.Gray,
            WidthRequest = 300
            //ThumbImageSource = "naidis.png"
        };
        slider.ValueChanged += Stepper_Slider_ValueChanged;
        al = new AbsoluteLayout { Children = { label, stepper, slider } };
        List<View> controls = new List<View> { label, stepper, slider };
        for (int i = 0; i < controls.Count; i++)
        {
            double yKoht = 0.2 + i * 0.2;
            AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, 0.2 + i * 0.2, 300, 60));
            AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);

        }
        Content = al;
    }
    private void Stepper_Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        label.Text = $"Stepperi/Slideri väärtus: {e.NewValue:F0}";
        label.FontSize = 24 + e.NewValue / 4;
        label.BackgroundColor = Color.FromRgb(
            (int)(e.NewValue * 2.50), 
            (int)(255 - e.NewValue + 2.55),
            128
        );
    }
}
