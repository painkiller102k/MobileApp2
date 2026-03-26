using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.Generic;

namespace MobileVerticalHorizontalApp;

public partial class Kontaktandmed : ContentPage
{
    TableView tabelview;
    SwitchCell sc;
    ImageCell ic;
    TableSection fotosection;

    EntryCell phoneCell;
    EntryCell emailCell;
    EntryCell messageCell;

    public Kontaktandmed()
    {
        InitializeComponent();

        sc = new SwitchCell { Text = "Näita veel" };
        sc.OnChanged += Sc_OnChanged;

        ic = new ImageCell
        {
            ImageSource = ImageSource.FromFile("peter_griffin.png"),
            Text = "Sõbra foto",
            Detail = "Kirjeldus"
        };

        fotosection = new TableSection();

        phoneCell = new EntryCell
        {
            Label = "Telefon",
            Placeholder = "Sisesta tel. number",
            Keyboard = Keyboard.Telephone
        };

        emailCell = new EntryCell
        {
            Label = "Email",
            Placeholder = "Sisesta email",
            Keyboard = Keyboard.Email
        };

        messageCell = new EntryCell
        {
            Label = "Sõnum",
            Placeholder = "Sisesta sõnum"
        };

        tabelview = new TableView
        {
            Intent = TableIntent.Form,
            Root = new TableRoot
            {
                new TableSection("Kontaktandmed:")
                {
                    phoneCell,
                    emailCell,
                    messageCell,
                    sc
                },

                fotosection,

                new TableSection("Lisavõimalused:")
                {
                    new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Button { Text="HELSTA", Command=new Command(Helista) },
                                new Button { Text="SMS", Command=new Command(SaadaSMS) },
                                new Button { Text="EMAIL", Command=new Command(SaadaEmail) }
                            }
                        }
                    },
                    new ViewCell
                    {
                        View = new Button
                        {
                            Text = "Õnnitlused",
                            Command = new Command(SaadaOnnitlus)
                        }
                    }
                }
            }
        };

        Content = tabelview;
    }

    private void Sc_OnChanged(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            fotosection.Title = "Foto:";
            fotosection.Add(ic);
            sc.Text = "Peida";
        }
        else
        {
            fotosection.Title = "";
            fotosection.Remove(ic);
            sc.Text = "Näita veel";
        }
    }

    private void Helista()
    {
        if (!string.IsNullOrWhiteSpace(phoneCell.Text))
            PhoneDialer.Open(phoneCell.Text);
    }

    private async void SaadaSMS()
    {
        if (Sms.Default.IsComposeSupported)
        {
            var sms = new SmsMessage(messageCell.Text, phoneCell.Text);
            await Sms.Default.ComposeAsync(sms);
        }
    }

    private async void SaadaEmail()
    {
        if (Email.Default.IsComposeSupported)
        {
            var email = new EmailMessage
            {
                Subject = "Tervitus",
                Body = messageCell.Text,
                To = new List<string> { emailCell.Text }
            };
            await Email.Default.ComposeAsync(email);
        }
    }

    private async void SaadaOnnitlus()
    {
        List<string> list = new List<string>
        {
            "Head jõule!",
            "Õnnelikku uut aastat!",
            "Palju õnne!",
            "Tervist ja edu!",
            "Ilusaid pühi!"
        };

        Random rnd = new Random();
        string msg = list[rnd.Next(list.Count)];

        bool sms = await DisplayAlert("Valik", "Saata SMS?", "Jah", "Ei");

        if (sms && Sms.Default.IsComposeSupported)
        {
            await Sms.Default.ComposeAsync(new SmsMessage(msg, phoneCell.Text));
        }
        else if (Email.Default.IsComposeSupported)
        {
            await Email.Default.ComposeAsync(new EmailMessage
            {
                Subject = "Õnnitlus",
                Body = msg,
                To = new List<string> { emailCell.Text }
            });
        }
    }
}