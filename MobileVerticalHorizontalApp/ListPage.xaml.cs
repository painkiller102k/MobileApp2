using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MobileVerticalHorizontalApp
{
    public class Riik
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvaarv { get; set; }
        public string Lipp { get; set; }
    }

    public class ListViewPage : ContentPage
    {
        ObservableCollection<Riik> riigid;
        ListView list;

        Entry entryNimi, entryPealinn, entryRahvaarv;

        Riik valitudRiik = null;

        string valitudLipp;

        public ListViewPage()
        {
            Title = "Rakendus maailma riikide kohta";

            riigid = new ObservableCollection<Riik>
            {
                new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=1300000, Lipp="estonia.png" },
                new Riik { Nimi="USA", Pealinn="Washington", Rahvaarv=7900000, Lipp="america.png" },
                new Riik { Nimi="UK", Pealinn="London", Rahvaarv=9000000, Lipp="uk.png" }
            };

            entryNimi = new Entry { Placeholder = "Riik" };
            entryPealinn = new Entry { Placeholder = "Pealinn" };
            entryRahvaarv = new Entry { Placeholder = "Rahvaarv", Keyboard = Keyboard.Numeric };

            var btnValiLipp = new Button
            {
                Text = "📷 Vali lipp",
                BackgroundColor = Colors.DarkBlue,
                TextColor = Colors.White,
                CornerRadius = 15
            };
            btnValiLipp.Clicked += BtnValiLipp_Clicked;

            var lblLipp = new Label
            {
                Text = "Vali pilt riigi jaoks !",
                TextColor = Colors.Gray,
                FontSize = 12
            };

            var btnLisa = new Button
            {
                Text = "➕ Lisa riik",
                BackgroundColor = Colors.Green,
                TextColor = Colors.White,
                CornerRadius = 15
            };
            btnLisa.Clicked += Lisa_Clicked;

            var btnKustuta = new Button
            {
                Text = "🗑 Kustuta",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                CornerRadius = 15
            };
            btnKustuta.Clicked += Kustuta_Clicked;

            var btnSalvesta = new Button
            {
                Text = "💾 Salvesta",
                BackgroundColor = Colors.Orange,
                TextColor = Colors.White,
                CornerRadius = 15
            };
            btnSalvesta.Clicked += Salvesta_Clicked;

            list = new ListView
            {
                ItemsSource = riigid,
                SelectionMode = ListViewSelectionMode.Single,
                HasUnevenRows = true
            };

            list.ItemTapped += List_ItemTapped;

            list.ItemTemplate = new DataTemplate(() =>
            {
                var img = new Image
                {
                    WidthRequest = 50,
                    HeightRequest = 30,
                    Aspect = Aspect.AspectFit
                };

                img.SetBinding(Image.SourceProperty, "Lipp");

                var nimi = new Label { FontAttributes = FontAttributes.Bold };
                nimi.SetBinding(Label.TextProperty, "Nimi");

                var pealinn = new Label { TextColor = Colors.Gray };
                pealinn.SetBinding(Label.TextProperty, "Pealinn");

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = 10,
                        Children =
                        {
                            img,
                            new StackLayout
                            {
                                Children = { nimi, pealinn }
                            }
                        }
                    }
                };
            });

            Content = new StackLayout
            {
                Padding = 10,
                Children =
                {
                    entryNimi,
                    entryPealinn,
                    entryRahvaarv,
                    btnValiLipp,
                    lblLipp,
                    btnLisa,
                    btnSalvesta,
                    btnKustuta,
                    list
                }
            };
        }

        private async void BtnValiLipp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.Default.PickPhotoAsync(); // photo emulator

                if (photo != null)
                {
                    valitudLipp = photo.FullPath;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Viga", ex.Message, "OK");
            }
        }

        private void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimi = entryNimi.Text;

            bool olemas = riigid.Any(r =>
                r.Nimi.Equals(nimi, StringComparison.OrdinalIgnoreCase)); // spisok nimi -> uus nimi

            if (olemas)
            {
                DisplayAlertAsync("Viga", "Riik on juba olemas!", "OK");
                return;
            }

            int.TryParse(entryRahvaarv.Text, out int arv);

            riigid.Add(new Riik
            {
                Nimi = entryNimi.Text,
                Pealinn = entryPealinn.Text,
                Rahvaarv = arv,
                Lipp = string.IsNullOrEmpty(valitudLipp) ? "default.png" : valitudLipp
            });

            Puhasta();
        }

        // kustuta
        private async void Kustuta_Clicked(object sender, EventArgs e)
        {
            if (valitudRiik == null)
            {
                await DisplayAlertAsync("Viga", "Vali riik!", "OK");
                return;
            }

            riigid.Remove(valitudRiik);
            Puhasta();
        }

        // salvesta
        private void Salvesta_Clicked(object sender, EventArgs e)
        {
            if (valitudRiik == null)
            {
                DisplayAlertAsync("Viga", "Vali riik!", "OK");
                return;
            }

            valitudRiik.Nimi = entryNimi.Text;
            valitudRiik.Pealinn = entryPealinn.Text;

            int.TryParse(entryRahvaarv.Text, out int arv);
            valitudRiik.Rahvaarv = arv;

            if (!string.IsNullOrEmpty(valitudLipp))
                valitudRiik.Lipp = valitudLipp;

            list.ItemsSource = null;
            list.ItemsSource = riigid;

            Puhasta();
        }

        // select
        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            valitudRiik = e.Item as Riik; // element spiska

            if (valitudRiik != null)
            {
                entryNimi.Text = valitudRiik.Nimi;
                entryPealinn.Text = valitudRiik.Pealinn;
                entryRahvaarv.Text = valitudRiik.Rahvaarv.ToString();

                valitudLipp = valitudRiik.Lipp;

                await DisplayAlertAsync("Info",
                    $"Riik: {valitudRiik.Nimi}\nPealinn: {valitudRiik.Pealinn}\nRahvaarv: {valitudRiik.Rahvaarv}",
                    "OK");
            }
        }

        private void Puhasta()
        {
            entryNimi.Text = "";
            entryPealinn.Text = "";
            entryRahvaarv.Text = "";

            valitudLipp = null;
            valitudRiik = null;
        }
    }
}