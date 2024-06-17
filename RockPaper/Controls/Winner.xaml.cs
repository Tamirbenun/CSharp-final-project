using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockPaper.Controls;

public partial class Winner : UserControl
{
    public Winner()
    {
        InitializeComponent();
    }

    private void Button_PlayAgain(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.StartGame();
    }

    private void Button_PlayNewGame(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.ComputerScore = 0;
        window.ScoreComputerText.Text = window.ComputerScore.ToString();
        window.PlayerScore = 0;
        window.ScorePlayerText.Text = window.PlayerScore.ToString();
        window.StartGame();
    }
}
