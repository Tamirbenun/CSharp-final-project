using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe;
using ToDo;
using ProjectGallery.Controls;

namespace ProjectGallery;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_InfoWeather(object sender, RoutedEventArgs e)
    {
        InfoWeather infoWeather = new InfoWeather();
        InfoControl.Content = infoWeather;
        Column.Width = new System.Windows.GridLength(350);
    }

    private void Button_InfoTicTacToe(object sender, RoutedEventArgs e)
    {
        InfoTicTacToe infoTicTacToe = new InfoTicTacToe();
        InfoControl.Content = infoTicTacToe;
        Column.Width = new System.Windows.GridLength(350);
    }

    private void Button_InfoToDo(object sender, RoutedEventArgs e)
    {
        InfoToDo infoToDo = new InfoToDo();
        InfoControl.Content = infoToDo;
        Column.Width = new System.Windows.GridLength(350);
    }

    private void Button_InfoCalculator(object sender, RoutedEventArgs e)
    {
        InfoCalculator infoCalculator = new InfoCalculator();
        InfoControl.Content = infoCalculator;
        Column.Width = new System.Windows.GridLength(350);
    }

    private void Button_InfoPong(object sender, RoutedEventArgs e)
    {
        InfoPong infoPong = new InfoPong();
        InfoControl.Content = infoPong;
        Column.Width = new System.Windows.GridLength(350);
    }

    private void Button_InfoRockPaper(object sender, RoutedEventArgs e)
    {
        InfoRockPaper infoRockPaper = new InfoRockPaper();
        InfoControl.Content = infoRockPaper;
        Column.Width = new System.Windows.GridLength(350);
    }
}