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
using System.Diagnostics;

namespace Pong.Controls;

public partial class Options : UserControl
{
    public double correntBallSpeed = 6;
    public double correntPaddleSpeed = 50;

    public string GameMode = "PvC";

    public Options()
    {
        InitializeComponent();

        rangeBallSpeed.ValueChanged += RangeBallSpeed_ValueChanged;
        rangePaddleSpeed.ValueChanged += RangePaddleSpeed_ValueChanged;

        correntBallSpeedTextBlock.Text = correntBallSpeed.ToString();
        rangeBallSpeed.Value = correntBallSpeed;

        correntPaddleSpeedTextBlock.Text = correntPaddleSpeed.ToString();
        rangePaddleSpeed.Value = correntPaddleSpeed;

        GameModeTextBlock.Text = "Player vs Computer";
    }

    private void RangeBallSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        correntBallSpeedTextBlock.Text = rangeBallSpeed.Value.ToString();
        correntBallSpeed = rangeBallSpeed.Value;
    }

    private void RangePaddleSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        correntPaddleSpeedTextBlock.Text = rangePaddleSpeed.Value.ToString();
        correntPaddleSpeed = rangePaddleSpeed.Value;
    }

    private void Button_Left(object sender, RoutedEventArgs e)
    {
        if (GameMode == "PvC")
        {
            GameModeTextBlock.Text = "Player vs Player";
            GameMode = "PvP";
        } else if (GameMode == "PvP")
        {
            GameModeTextBlock.Text = "Player vs Computer";
            GameMode = "PvC";
        }
    }

    private void Button_Right(object sender, RoutedEventArgs e)
    {
        if (GameMode == "PvC")
        {
            GameModeTextBlock.Text = "Player vs Player";
            GameMode = "PvP";
        } else if (GameMode == "PvP")
        {
            GameModeTextBlock.Text = "Player vs Computer";
            GameMode = "PvC";
        }
    }

    private void Button_RestOption(object sender, RoutedEventArgs e)
    {
        correntBallSpeed = 6;
        rangeBallSpeed.Value = correntBallSpeed;
        correntPaddleSpeed = 50;
        rangePaddleSpeed.Value = correntPaddleSpeed;
        GameMode = "PvC";
        GameModeTextBlock.Text = "Player vs Computer";
    }

    private void Button_Back(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this) as MainWindow;
        window.ContentControlNow.Content = window.main;
        window.setballSpeed = correntBallSpeed;
        window.paddleSpeed = correntPaddleSpeed;
        window.gameMode = GameMode;
        this.Visibility = Visibility.Hidden;
        window.main.Visibility = Visibility.Visible;
    }
}
