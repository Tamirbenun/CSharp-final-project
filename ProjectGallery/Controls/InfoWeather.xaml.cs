using System.Windows;
using System.Windows.Controls;

namespace ProjectGallery.Controls;

public partial class InfoWeather : UserControl
{
    Weather.MainWindow WeatherProject;
    public InfoWeather()
    {
        InitializeComponent();
    }

    private void Button_RunWeather(object sender, RoutedEventArgs e)
    {
        WeatherProject = new Weather.MainWindow();
        WeatherProject.Show();
    }

    private void Button_InfoWeatherClose(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        var window = Window.GetWindow(this) as MainWindow;
        window.Column.Width = new System.Windows.GridLength(0);
    }
}
