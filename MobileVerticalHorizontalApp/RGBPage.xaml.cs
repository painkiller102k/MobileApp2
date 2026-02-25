namespace MobileVerticalHorizontalApp;

public partial class RGBPage : ContentPage
{
    public RGBPage()
    {
        InitializeComponent();
        UpdateColor();
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        int r = Convert.ToInt32(redSlider.Value);
        int g = Convert.ToInt32(greenSlider.Value);
        int b = Convert.ToInt32(blueSlider.Value);

        redValueLabel.Text = "Red = " + r.ToString();
        greenValueLabel.Text = "Green = " + g.ToString();
        blueValueLabel.Text = "Blue = " + b.ToString();

        redFrame.BackgroundColor = Color.FromRgb(r, 0, 0);
        greenFrame.BackgroundColor = Color.FromRgb(0, g, 0);
        blueFrame.BackgroundColor = Color.FromRgb(0, 0, b);

        colorBox.BackgroundColor = Color.FromRgb(r, g, b);
        RGBLabel.TextColor = Color.FromRgb(r, g, b);
    }

    void OnRandomClicked(object sender, EventArgs e)
    {
        Random rnd = new Random();

        redSlider.Value = rnd.Next(0, 256);
        greenSlider.Value = rnd.Next(0, 256);
        blueSlider.Value = rnd.Next(0, 256);

        UpdateColor();
    }
}