namespace MobileVerticalHorizontalApp;

public partial class MainPage : ContentPage
{
    private AnimalViewModel vm = new AnimalViewModel();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OnPoderClicked(object sender, EventArgs e)
    {
        vm.ChangeAnimal("Põder");
    }

    private void OnOravClicked(object sender, EventArgs e)
    {
        vm.ChangeAnimal("Orav");
    }

    private void OnSiilClicked(object sender, EventArgs e)
    {
        vm.ChangeAnimal("Siil");
    }
}