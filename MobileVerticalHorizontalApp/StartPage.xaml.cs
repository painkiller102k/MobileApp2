using System.Threading.Tasks;

namespace MobileVerticalHorizontalApp;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	ScrollView sv;
	public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new FigurePage() };
	public List<string> LeheNimed = new List<string>() { "Tekst", "Kujund" };
	public StartPage()
	{
		Title = "Avaleht";
		vst = new VerticalStackLayout { Padding = 20, Spacing = 15 };
		for (int i = 0; i < Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = LeheNimed[1],
				FontSize = 30,
				FontFamily = "Longs",
                BackgroundColor = Colors.WhiteSmoke,
				TextColor = Colors.Black,
				CornerRadius = 10,
				HeightRequest = 50,
				ZIndex = i
			};
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
			//nupp.Clicked += Nupp_Clicked;
		}
		sv = new ScrollView { Content=vst};
		Content = sv;
	}
	//private async Task Nupp_Clicked(object? sender, EventArgs e)
	//{
	//	Button nupp = sender as Button;
	//	await Navigation.PushAsync(Lehed[nupp.ZIndex]);
	//}
}