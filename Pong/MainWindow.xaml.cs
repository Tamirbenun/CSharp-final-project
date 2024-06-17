using System.Media;
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
using System.Windows.Threading;
using Pong.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pong;
public partial class MainWindow : Window
{
    public Main main = new Main();
    public Winner winner = new Winner();
    Options options = new Options();

    public Rectangle paddle1;
    public Rectangle paddle2;
    public double paddleSpeed = 50;

    public Border ball;
    public double ballSpeedX = 6;
    public double ballSpeedY = 6;
    public double rememberSpeedX;
    public double rememberSpeedY;
    public double setballSpeed = 6;

    public DispatcherTimer gameTimer1;
    public DispatcherTimer gameTimer2;

    public int player1Score = 0;
    public int player2Score = 0;

    public string gameMode = "PvC";
    
    public bool isActive = false;

    public MainWindow()
    {
        InitializeComponent();
        ContentControlNow.Content = main;
    }

    public void StartGame()
    {
        BoardDisplay();

        if (ballSpeedX != setballSpeed && ballSpeedY != setballSpeed)
        {
            ballSpeedX = setballSpeed;
            ballSpeedY = setballSpeed;
        }

        if (!isActive)
        {
            Loaded += new RoutedEventHandler(WindowLoaded);
            SizeChanged += new SizeChangedEventHandler(HandleWindowSizeChanged);
            KeyDown += new KeyEventHandler(HandleKeyDown);
        }

        gameTimer1 = new DispatcherTimer();
        gameTimer1.Interval = TimeSpan.FromMilliseconds(16);
        gameTimer1.Tick += new EventHandler(GameLoop);
        gameTimer1.Start();

        if (gameMode == "PvC")
        {
            gameTimer2 = new DispatcherTimer();
            gameTimer2.Interval = TimeSpan.FromMilliseconds(200);
            gameTimer2.Tick += new EventHandler(ComputerLoop);
            gameTimer2.Start();
        }

        SetInitialPositions();

        rememberSpeedX = ballSpeedX;
        rememberSpeedY = ballSpeedY;

        isActive = true;
    }

    public void BoardDisplay()
    {
        if (player1Score == 0 && player2Score == 0)
        {
            PlayerScore1.Text = "00";
            PlayerScore2.Text = "00";
        }

        LineBoard.Stroke = Brushes.Gray;
        ButtonExite.Visibility = Visibility.Visible;
        ButtonPause.Visibility = Visibility.Visible;

        paddle1 = CreatePaddle();
        paddle2 = CreatePaddle();
        ball = CreateBall();

        GameCanvas.Children.Add(paddle1);
        GameCanvas.Children.Add(paddle2);
        GameCanvas.Children.Add(ball);
    }

    public void GameLoop(object? sender, EventArgs e)
    {
        Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballSpeedX);
        Canvas.SetTop(ball, Canvas.GetTop(ball) + ballSpeedY);

        // Ball collision with top and bottom walls
        if (Canvas.GetTop(ball) <= 0 || Canvas.GetTop(ball) >= GameCanvas.ActualHeight - ball.Height)
        {
            ballSpeedY = -ballSpeedY;
            rememberSpeedY = ballSpeedY;
        }

        // Ball collision with paddles
        if (Canvas.GetLeft(ball) <= Canvas.GetLeft(paddle1) + paddle1.Width &&
            Canvas.GetTop(ball) + ball.Height >= Canvas.GetTop(paddle1) &&
            Canvas.GetTop(ball) <= Canvas.GetTop(paddle1) + paddle1.Height)
        {
            ballSpeedX = -ballSpeedX;
            rememberSpeedX = ballSpeedX;
        }

        if (Canvas.GetLeft(ball) >= Canvas.GetLeft(paddle2) - ball.Width &&
            Canvas.GetTop(ball) + ball.Height >= Canvas.GetTop(paddle2) &&
            Canvas.GetTop(ball) <= Canvas.GetTop(paddle2) + paddle2.Height)
        {
            ballSpeedX = -ballSpeedX;
            rememberSpeedX = ballSpeedX;
        }

        // Ball out of bounds (left or right side)
        if (Canvas.GetLeft(ball) <= 0)
        {
            PlayersUpdateScore(1);
            ChackWin();
            if (player1Score < 10 && player2Score < 10)
            {
                ResetBall();
            }
            

        } else if (Canvas.GetLeft(ball) >= GameCanvas.ActualWidth - ball.Width)
        {
            PlayersUpdateScore(2);
            ChackWin();
            if (player1Score < 10 && player2Score < 10)
            {
                ResetBall();
            }
        }
    }

    public void ComputerLoop(object? sender, EventArgs e)
    {
        if (gameMode == "PvC")
        {
            double paddle1Top = Canvas.GetTop(paddle1);
            double ballTop = Canvas.GetTop(ball);

            Random rnd = new Random();
            int humanErrors = rnd.Next(60, 130);
            Canvas.SetTop(paddle1, ballTop - humanErrors);
        }
    }

    public void ChackWin()
    {
        if (player1Score == 10 || player2Score == 10)
        {
            gameTimer1.Stop();
            gameTimer2.Stop();
            ballSpeedX = 0;
            ballSpeedY = 0;

            if (player1Score == 10 && gameMode == "PvC")
            {
                winner.WhoWin.Text = "You";
            }
            else if (player1Score == 10 && gameMode == "PvP")
            {
                winner.WhoWin.Text = "Player 1";
            } else if (player2Score == 10 && gameMode == "PvC")
            {
                winner.WhoWin.Text = "Computer";
            }
            else if (player2Score == 10 && gameMode == "PvP")
            {
                winner.WhoWin.Text = "Player 2";
            }


            ContentControlNow.Content = winner;
            winner.Visibility = Visibility.Visible;
        }
    }



    private void ResetBall()
    {
        Canvas.SetLeft(ball, (GameCanvas.ActualWidth - ball.Width) / 2);
        Canvas.SetTop(ball, (GameCanvas.ActualHeight - ball.Height) / 2);
        ballSpeedX = setballSpeed;
        ballSpeedY = setballSpeed;
        rememberSpeedX = ballSpeedX;
        rememberSpeedY = ballSpeedY;
    }

    private void PlayersUpdateScore(int player)
    {
        if (player == 1)
        {
            player1Score++;
            if (player1Score < 10)
            {
                PlayerScore1.Text = "0" + player1Score.ToString();
            } else
            {
                PlayerScore1.Text = player1Score.ToString();
            }
        }
        else if (player == 2)
        {
            player2Score++;
            if (player2Score < 10)
            {
                PlayerScore2.Text = "0" + player2Score.ToString();
            } else
            {
                PlayerScore2.Text = player2Score.ToString();
            }
        }
    }

    private void SetInitialPositions()
    {
        Canvas.SetLeft(paddle1, 50);
        Canvas.SetTop(paddle1, (GameCanvas.ActualHeight / 2) - (paddle1.ActualHeight));

        Canvas.SetLeft(paddle2, GameCanvas.ActualWidth - 50);
        Canvas.SetTop(paddle2, (GameCanvas.ActualHeight / 2) - (paddle2.ActualHeight));

        Canvas.SetLeft(ball, (GameCanvas.ActualWidth - ball.ActualWidth) / 2);
        Canvas.SetTop(ball, (GameCanvas.ActualHeight - ball.ActualHeight) / 2);
    }

    private void HandleKeyDown(object sender, KeyEventArgs e)
    {
        double p1Top = Canvas.GetTop(paddle1);
        double p2Top = Canvas.GetTop(paddle2);

        if (e.Key == Key.Up && p2Top > 0)
        {
            Canvas.SetTop(paddle2, p2Top - paddleSpeed);
        }
        if (e.Key == Key.Down && p2Top < (GameCanvas.ActualHeight - paddle2.ActualHeight))
        {
            Canvas.SetTop(paddle2, p2Top + paddleSpeed);
        }

        if (gameMode == "PvP")
        {
            if (e.Key == Key.W && p1Top > 0)
            {
                Canvas.SetTop(paddle1, p1Top - paddleSpeed);
            }
            if (e.Key == Key.S && p1Top < (GameCanvas.ActualHeight - paddle1.ActualHeight))
            {
                Canvas.SetTop(paddle1, p1Top + paddleSpeed);
            }
        }
       
    }

    private Rectangle CreatePaddle()
    {
        return new Rectangle()
        {
            Width = 10,
            Height = 100,
            Fill = Brushes.White
        };
    }

    private Border CreateBall()
    {
        return new Border
        {
            Width = 15,
            Height = 15,
            CornerRadius = new CornerRadius(50),
            Background = Brushes.White,
        };
    }

    private void HandleWindowSizeChanged(object sender, SizeChangedEventArgs e)
    {
        SetInitialPositions();
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        SetInitialPositions();
    }

    private void ButtonPause_Click(object sender, RoutedEventArgs e)
    {
        if (ballSpeedX != 0 && ballSpeedY != 0)
        {
            ballSpeedX = 0;
            ballSpeedY = 0;
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Play.png", UriKind.RelativeOrAbsolute));
            PauseImageButton.Source = bitmapImage;
            gameTimer2.Stop();
        } else if (ballSpeedX == 0 && ballSpeedY == 0)
        {
            ballSpeedX = rememberSpeedX;
            ballSpeedY = rememberSpeedY;
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pause.png", UriKind.RelativeOrAbsolute));
            PauseImageButton.Source = bitmapImage;
            gameTimer2.Start();
        }
    }

    private void ButtonExite_Click(object sender, RoutedEventArgs e)
    {
        player1Score = 0;
        player2Score = 0;
        PlayerScore1.Text = "";
        PlayerScore2.Text = "";

        LineBoard.Stroke = Brushes.Black;
        ButtonExite.Visibility = Visibility.Hidden;
        ButtonPause.Visibility = Visibility.Hidden;

        ballSpeedX = 0;
        ballSpeedY = 0;

        GameCanvas.Children.Clear();

        gameTimer1.Stop();
        gameTimer2.Stop();

        ContentControlNow.Content = main;
        main.Visibility = Visibility.Visible;
    }
}