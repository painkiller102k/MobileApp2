using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileVerticalHorizontalApp;

public class AnimalViewModel : INotifyPropertyChanged
{
    private string _currentAnimalImage;

    public string CurrentAnimalImage
    {
        get => _currentAnimalImage;
        set
        {
            _currentAnimalImage = value;
            OnPropertyChanged();
        }
    }

    public AnimalViewModel()
    {
        // начальное изображение
        CurrentAnimalImage = Resources.AppResources.Siil;
    }

    public void ChangeAnimal(string type)
    {
        CurrentAnimalImage = type switch
        {
            "Põder" => Resources.AppResources.Poder,
            "Orav" => Resources.AppResources.Orav,
            _ => Resources.AppResources.Siil
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}