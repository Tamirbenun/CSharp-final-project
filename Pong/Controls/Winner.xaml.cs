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
using Pong.Controls;
using Pong;
using System.Windows.Controls.Primitives;

namespace Pong.Controls;

public partial class Winner : UserControl
{
    public Winner()
    {
        InitializeComponent();
        
    }

    private void Button_Yes(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.player1Score = 0;
        window.player2Score = 0;
        window.GameCanvas.Children.Clear();
        window.StartGame();
        this.Visibility = Visibility.Collapsed;
    }

    private void Button_No(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.player1Score = 0;
        window.player2Score = 0;
        window.PlayerScore1.Text = "";
        window.PlayerScore2.Text = "";
        window.LineBoard.Stroke = Brushes.Black;
        window.ButtonExite.Visibility = Visibility.Hidden;
        window.ButtonPause.Visibility = Visibility.Hidden;
        window.ballSpeedX = 0;
        window.ballSpeedY = 0;
        window.GameCanvas.Children.Clear();
        window.gameTimer1.Stop();
        window.gameTimer2.Stop();
        window.ContentControlNow.Content = window.main;
        window.main.Visibility = Visibility.Visible;
        this.Visibility = Visibility.Collapsed;
    }
}
