using RockPaper.Controls;
using System;
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

namespace RockPaper;
public partial class MainWindow : Window
{
    private DispatcherTimer GameTimer;
    public int GameTimerTime = 2;

    private DispatcherTimer HandLeftTimer;
    private DispatcherTimer HandRightTimer;
    private bool HandLeftIsDown = true;
    private bool HandRightIsDown = true;

    private Random ComputerRandomMoove = new Random();
    private Random PlayerRandomMoove = new Random();

    private string ComputerHand = "";
    private string PlayerHand = "";

    public int ComputerScore = 0;
    public int PlayerScore = 0;

    private bool GameIsActive = false;
    private bool GameIsRunningBefore = false;

    public MainWindow()
    {
        InitializeComponent();
        ButtonStartDisplay();
    }

    private void ButtonStartDisplay()
    {
        var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
        Button button = new Button();
        button.Width = 210;
        button.Height = 60;
        button.Background = Brushes.White;
        button.BorderThickness = new Thickness(0);
        button.Content = "Let's Play!!";
        button.FontSize = 40;
        button.FontWeight = FontWeights.Medium;
        button.Foreground = purple;
        button.Click += Button_Play;

        MiddleContent.Content = button;
    }

    public void Button_Play(object sender, RoutedEventArgs e)
    {
        StartGame();
    }

    public void StartGame()
    {
        GameIsActive = true;

        GameTimerTime = 2;
        ComputerHand = "";
        PlayerHand = "";
        BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockL.png", UriKind.RelativeOrAbsolute));
        HandLeftImage.Source = bitmapImage;
        BitmapImage bitmapImage2 = new BitmapImage(new Uri("Resources/RockR.png", UriKind.RelativeOrAbsolute));
        HandRightImage.Source = bitmapImage2;
        MarkRock.Background = Brushes.White;
        MarkPaper.Background = Brushes.White;
        MarkScissors.Background = Brushes.White;

        var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
        TextBlock textBlock = new TextBlock();
        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        textBlock.VerticalAlignment = VerticalAlignment.Center;
        textBlock.FontSize = 40;
        textBlock.FontWeight = FontWeights.Bold;
        textBlock.Foreground = purple;
        textBlock.Text = "3";
        MiddleContent.Content = textBlock;

        if (!GameIsRunningBefore)
        {
            GameTimer = new DispatcherTimer();
            GameTimer.Interval = TimeSpan.FromMilliseconds(1000);
            GameTimer.Tick += new EventHandler(CountDown);

            HandLeftTimer = new DispatcherTimer();
            HandLeftTimer.Interval = TimeSpan.FromMilliseconds(1);
            HandLeftTimer.Tick += new EventHandler(HandLeftEffect);

            HandRightTimer = new DispatcherTimer();
            HandRightTimer.Interval = TimeSpan.FromMilliseconds(1);
            HandRightTimer.Tick += new EventHandler(HandRightEffect);
        }

        GameTimer.Start();
        HandLeftTimer.Start();
        HandRightTimer.Start();

        GameIsRunningBefore = true;
    }

    public void GameTimerDisplay()
    {
        var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
        TextBlock GameTimerText = new TextBlock();
        GameTimerText.HorizontalAlignment = HorizontalAlignment.Center;
        GameTimerText.VerticalAlignment = VerticalAlignment.Center;
        GameTimerText.FontSize = 40;
        GameTimerText.FontWeight = FontWeights.Bold;
        GameTimerText.Foreground = purple;
        GameTimerText.Text = GameTimerTime.ToString();

        MiddleContent.Content = GameTimerText;
    }

    public void CountDown(object? sender, EventArgs e)
    {
        GameTimerDisplay();

        GameTimerTime -= 1;

        if (GameTimerTime < 0)
        {
            GameTimer.Stop();
            HandLeftTimer.Stop();
            HandRightTimer.Stop();
            ComputerMove();

            if (PlayerHand == "Rock")
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockR.png", UriKind.RelativeOrAbsolute));
                HandRightImage.Source = bitmapImage;
            }
            else if (PlayerHand == "Paper")
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/PaperR.png", UriKind.RelativeOrAbsolute));
                HandRightImage.Source = bitmapImage;
            }
            else if (PlayerHand == "Scissors")
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/ScissorsR.png", UriKind.RelativeOrAbsolute));
                HandRightImage.Source = bitmapImage;
            }
            else if (PlayerHand == "")
            {
                PlayerMove();
            }

            CheckWin();

            GameIsActive = false;
        }
    }

    public void HandLeftEffect(object? sender, EventArgs e)
    {
        RotateTransform rotateTransform = ((RotateTransform)((TransformGroup)HandLeftImage.RenderTransform).Children.First(tr => tr is RotateTransform));

        if (rotateTransform.Angle >= 0 && rotateTransform.Angle < 10 && HandLeftIsDown)
        {
            rotateTransform.Angle += 0.5;

            if (rotateTransform.Angle == 10)
            {
                HandLeftIsDown = false;
            }
        }
        else if (!HandLeftIsDown)
        {
            rotateTransform.Angle -= 0.5;

            if (rotateTransform.Angle == 0)
            {
                HandLeftIsDown = true;
            }
        }
    }

    private void HandRightEffect(object? sender, EventArgs e)
    {
        RotateTransform rotateTransform = ((RotateTransform)((TransformGroup)HandRightImage.RenderTransform).Children.First(tr => tr is RotateTransform));

        if (rotateTransform.Angle <= 0 && rotateTransform.Angle > -10 && HandRightIsDown)
        {
            rotateTransform.Angle -= 0.5;

            if (rotateTransform.Angle  == -10)
            {
                HandRightIsDown = false;
            }
        } else if (!HandRightIsDown)
        {
            rotateTransform.Angle += 0.5;

            if (rotateTransform.Angle == 0)
                {
                HandRightIsDown = true;
                }
        }
    }

    private void PlayerMove()
    {
        int randomMove = PlayerRandomMoove.Next(1, 6);
        int rnd = PlayerRandomMoove.Next(20);

        for (int i = 1; i < rnd; i++)
        {
            randomMove = PlayerRandomMoove.Next(1, 6);
        }

        if (randomMove == 1)
        {
            PlayerHand = "Rock";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        } else if (randomMove == 2)
        {
            PlayerHand = "Paper";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/PaperR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        } else if (randomMove == 3)
        {
            PlayerHand = "Scissors";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/ScissorsR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        } else if (randomMove == 4)
        {
            PlayerHand = "Scissors";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/ScissorsR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        } else if (randomMove == 5)
        {
            PlayerHand = "Paper";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/PaperR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        } else if (randomMove == 6)
        {
            PlayerHand = "Rock";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockR.png", UriKind.RelativeOrAbsolute));
            HandRightImage.Source = bitmapImage;
        }
    }

    private void ComputerMove()
    {
        int randomMove = ComputerRandomMoove.Next(1, 6);

        if (randomMove == 1)
        {
            ComputerHand = "Rock";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        } else if (randomMove == 2)
        {
            ComputerHand = "Paper";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/PaperL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        } else if (randomMove == 3) {
            ComputerHand = "Scissors";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/ScissorsL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        } else if (randomMove == 4)
        {
            ComputerHand = "Scissors";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/ScissorsL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        } else if (randomMove == 5)
        {
            ComputerHand = "Paper";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/PaperL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        } else if (randomMove == 6)
        {
            ComputerHand = "Rock";
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/RockL.png", UriKind.RelativeOrAbsolute));
            HandLeftImage.Source = bitmapImage;
        }
    }

    private void WinDisplay(string winner)
    {
        Winner WinnerControl = new Winner();
        WinnerControl.WinDisplayText.Text = winner;

        MiddleContent.Content = WinnerControl;
    }

    private void CheckWin()
    {
        if (PlayerHand == "Rock" && ComputerHand == "Rock")
        {
            WinDisplay("DRAW");
        } else if (PlayerHand == "Rock" && ComputerHand == "Paper")
        {
            ComputerScore += 1;
            ScoreComputerText.Text = ComputerScore.ToString();
            WinDisplay("You Lose :(");
        } else if (PlayerHand == "Rock" && ComputerHand == "Scissors")
        {
            PlayerScore += 1;
            ScorePlayerText.Text = PlayerScore.ToString();
            WinDisplay("You WIN :)");
        } else if (PlayerHand == "Paper" && ComputerHand == "Rock")
        {
            PlayerScore += 1;
            ScorePlayerText.Text = PlayerScore.ToString();
            WinDisplay("You WIN :)");
        } else if (PlayerHand == "Paper" && ComputerHand == "Paper")
        {
            WinDisplay("DRAW");
        } else if (PlayerHand == "Paper" && ComputerHand == "Scissors")
        {
            ComputerScore += 1;
            ScoreComputerText.Text = ComputerScore.ToString();
            WinDisplay("You Lose :(");
        } else if (PlayerHand == "Scissors" && ComputerHand == "Rock")
        {
            ComputerScore += 1;
            ScoreComputerText.Text = ComputerScore.ToString();
            WinDisplay("You Lose :(");
        } else if (PlayerHand == "Scissors" && ComputerHand == "Paper")
        {
            PlayerScore += 1;
            ScorePlayerText.Text = PlayerScore.ToString();
            WinDisplay("You WIN :)");
        } else if (PlayerHand == "Scissors" && ComputerHand == "Scissors")
        {
            WinDisplay("DRAW");
        }
    }

    private void Button_Rock(object sender, RoutedEventArgs e)
    {
        if (GameIsActive)
        {
            PlayerHand = "Rock";
            var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
            MarkRock.Background = purple;
            MarkPaper.Background = Brushes.White;
            MarkScissors.Background = Brushes.White;
        }
    }

    private void Button_Paper(object sender, RoutedEventArgs e)
    {
        if (GameIsActive)
        {
            PlayerHand = "Paper";
            var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
            MarkPaper.Background = purple;
            MarkRock.Background = Brushes.White;
            MarkScissors.Background = Brushes.White;
        }
    }

    private void Button_Scissors(object sender, RoutedEventArgs e)
    {
        if (GameIsActive)
        {
            PlayerHand = "Scissors";
            var purple = (Brush)new BrushConverter().ConvertFromString("#7C29EB");
            MarkScissors.Background = purple;
            MarkRock.Background = Brushes.White;
            MarkPaper.Background = Brushes.White;
        }
    }
}