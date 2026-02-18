namespace MobileVerticalHorizontalApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var startPage = new StartPage();
            var navPage = new NavigationPage(startPage)
            {
                BarBackgroundColor = Colors.Blue,
                BarTextColor = Colors.White
            };

            MainPage = navPage;
        }
    }
}
