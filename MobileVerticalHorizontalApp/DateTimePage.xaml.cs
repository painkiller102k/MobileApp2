using Microsoft.Maui.Layouts;

namespace MobileVerticalHorizontalApp;

public partial class DateTimePage : ContentPage
{
    DatePicker datePicker;
    TimePicker timePicker;
    Label datetimeLabel;
    AbsoluteLayout al;

    public DateTimePage()
    {
        datePicker = new DatePicker
        {
            MinimumDate = DateTime.Now.AddDays(-15),
            MaximumDate = DateTime.Now.AddDays(15),
            Date = DateTime.Now,
            HorizontalOptions = LayoutOptions.Center,
            Format = "D"
        };
        datePicker.DateSelected += (sender, e) =>
        {
            datetimeLabel.Text = $"Valitud Kuupäev : {datePicker.Date:D}";
        };

        timePicker = new TimePicker
        {
            Time = DateTime.Now.TimeOfDay,
            HorizontalOptions = LayoutOptions.Center,
            Format = "T"
        };
        timePicker.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                datetimeLabel.Text = $"Valitud kelleaeg  : {timePicker.Time:P}";
            }
            else if (e.PropertyName==DatePicker.DateProperty.PropertyName)
            {
                datetimeLabel.Text = $"Valitud kuupäev : {datePicker.Date:D}";
            }
            else
            {
                datetimeLabel.Text = $"Vali kuupäev või aeg !";
            }
        };
        datetimeLabel = new Label
        {
            Text = "Vali kuupäev või aeg",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        al = new AbsoluteLayout { Children = { datePicker, timePicker, datetimeLabel } };

        AbsoluteLayout.SetLayoutBounds(datetimeLabel, new Rect(0.5, 0.2, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

        AbsoluteLayout.SetLayoutBounds(datePicker, new Rect(0.5, 0.4, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

        AbsoluteLayout.SetLayoutBounds(timePicker, new Rect(0.5, 0.6, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

        List<View> controls = new List<View> { datePicker, timePicker, datetimeLabel };
        for (int i = 0; i < controls.Count; i++)
        {
            double yKoht = 0.2 + i * 0.2;
            AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);
        }
        Content = al;
    }
}
