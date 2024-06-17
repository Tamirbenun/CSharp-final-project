using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls;
using TicTacToe.Enums;

namespace TicTacToe.Controls;

public partial class Board : UserControl, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public EventHandler<GameEndEventArgs> GameEnded;

    private const string PlayerOneContent = "X";
    private const string PlayerTwoContent = "O";

    private readonly Button[,] _buttons = new Button[3, 3];

    private readonly Random _rnd = new Random();

    private bool _isPlayerOneTurn = true;
    private bool _gameIsActive = true;
    private GameType _gameType = GameType.PvP;
    private Player1Type _player1Type = Player1Type.Player1;
    private Player2Type _player2Type = Player2Type.Player2;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private string endGameState;

    public Board()
    {
        InitializeComponent();

        InitializeGameGrid();

        GameEnded += HandleGameEnded;

        DataContext = this;
    }

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void HandleGameEnded(object? sender, GameEndEventArgs e)
    {
        switch (e.GameResult)
        {
            case GameResult.PlayerOneWins:
                PlayerOneScore++;
                EndGameState = "Player 1 won!";
                break;
            case GameResult.PlayerTwoWins:
                PlayerTwoScore++;
                EndGameState = "Player 2 won!";
                break;
            case GameResult.Draw:
                EndGameState = "Draw!";
                break;
        }
    }

    public string EndGameState
    {
        get => endGameState;
        set
        {
            endGameState = value;
            OnPropertyChanged(nameof(EndGameState));
        }
    }

    public int PlayerOneScore
    {
        get => playerOneScore;
        set
        {
            playerOneScore = value;
            OnPropertyChanged(nameof(PlayerOneScore));
        }
    }

    public int PlayerTwoScore
    {
        get => playerTwoScore;
        set
        {
            playerTwoScore = value;
            OnPropertyChanged(nameof(PlayerTwoScore));
        }
    }

    private void OnGameEnd(GameResult result)
    {
        GameEnded?.Invoke(this, new GameEndEventArgs(result));
    }

    public string PlayerOneTurn => IsPlayerOneTurn ? "Bold" : "Regular";
    public string PlayerTwoTurn => !IsPlayerOneTurn ? "Bold" : "Regular";
    public bool IsPlayerOneTurn
    {
        get => _isPlayerOneTurn;
        set
        {
            _isPlayerOneTurn = value;
            OnPropertyChanged(nameof(PlayerOneTurn));
            OnPropertyChanged(nameof(PlayerTwoTurn));
        }
    }

    public Player1Type CurrentPlayer1Type
    {
        get => _player1Type;
        set
        {
            _player1Type = value;
            OnPropertyChanged(nameof(CurrentPlayer1Type));
        }
    }

    public Player2Type CurrentPlayer2Type
    {
        get => _player2Type;
        set
        {
            _player2Type = value;
            OnPropertyChanged(nameof(CurrentPlayer2Type));
        }
    }

    public GameType CurrentGameType
    {
        get => _gameType;
        set
        {
            _gameType = value;
            OnPropertyChanged(nameof(CurrentGameType));
        }
    }



    private void InitializeGameGrid()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Button btn = new Button()
                {
                    FontSize = 40,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(2),
                    BorderThickness = new Thickness(0),
                    Background = Brushes.White

                };

                btn.Click += Button_Click;

                Grid.SetRow(btn, x);
                Grid.SetColumn(btn, y);

                _buttons[x, y] = btn;

                GameGrid.Children.Add(btn);
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!_gameIsActive || (CurrentGameType == GameType.PvC && !IsPlayerOneTurn) || CurrentGameType == GameType.CvC)
        {
            return;
        }

        Button btn = sender as Button;
        if (btn == null)
        {
            return;
        }

        if (btn.Content == null)
        {
            btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;

            if (ProcessEndGame())
            {
                return;
            }

            IsPlayerOneTurn = !IsPlayerOneTurn;

            if (CurrentGameType == GameType.PvC && !IsPlayerOneTurn)
            {
                ComputerMove();
            }
        }
    }

    private void ComputerMove()
    {
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(_rnd.Next(10) / 10.0)
        };
        timer.Tick += (sender, e) => {
            timer.Stop();
 
            Button btn;
            do
            {
                int row = _rnd.Next(3);
                int col = _rnd.Next(3);
                btn = _buttons[row, col];
            } while (btn.Content != null);


            btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
            if (ProcessEndGame())
            {
                return;
            }

            IsPlayerOneTurn = !IsPlayerOneTurn;

            if (CurrentGameType == GameType.CvC && !IsBoardFull())
            {
                ComputerMove();
            }
        };
        timer.Start();
    }

    private bool ProcessEndGame()
    {
        if (CheckForWinner())
        {
            GameResult result = IsPlayerOneTurn ? GameResult.PlayerOneWins : GameResult.PlayerTwoWins;

            OnGameEnd(result);

            _gameIsActive = false;

            MessageBoxResult resultPlayer = MessageBox.Show($"{result.ToString()} \n\n You Want Play Again?", "", MessageBoxButton.YesNo);

            if (resultPlayer == MessageBoxResult.No)
            {
            } else
            {
                StartNewGame(CurrentGameType, CurrentPlayer1Type, CurrentPlayer2Type);
            }
            

            return true;
        }

        if (IsBoardFull())
        {
            GameResult result = GameResult.Draw;

            OnGameEnd(result);

            _gameIsActive = false;

            if (result.ToString() == "PlayerOneWins")
            {

            }

            MessageBoxResult resultPlayer = MessageBox.Show($"{result.ToString()} \n\n You Want Play Again?", "", MessageBoxButton.YesNo);

            if (resultPlayer == MessageBoxResult.No)
            {
            }
            else
            {
                StartNewGame(CurrentGameType, CurrentPlayer1Type, CurrentPlayer2Type);
            }

            return true;
        }

        return false;
    }

    public void StartNewGame(GameType gameType, Player1Type player1Type, Player2Type player2Type)
    {
        if (_gameIsActive && CurrentGameType == GameType.CvC)
        {
            return;
        }
        CurrentGameType = gameType;
        CurrentPlayer1Type = player1Type;
        CurrentPlayer2Type = player2Type;

        IsPlayerOneTurn = true;
        _gameIsActive = true;

        foreach (Button btn in _buttons)
        {
            btn.Content = null;
        }

        if (CurrentGameType == GameType.CvC)
        {
            ComputerMove();
        }
    }

    private bool IsBoardFull()
    {
        foreach (Button button in _buttons)
        {
            if (button.Content == null)
            {
                return false;
            }
        }

        return true;
    }

    private bool AreButtonsEqual(Button b1, Button b2, Button b3) =>
         b1.Content != null && b1.Content == b2.Content && b2.Content == b3.Content;

    private bool CheckForWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (AreButtonsEqual(_buttons[i, 0], _buttons[i, 1], _buttons[i, 2]))
            {
                return true;
            }

            if (AreButtonsEqual(_buttons[0, i], _buttons[1, i], _buttons[2, i]))
            {
                return true;
            }
        }

        if (AreButtonsEqual(_buttons[0, 0], _buttons[1, 1], _buttons[2, 2]))
        {
            return true;
        }
        if (AreButtonsEqual(_buttons[0, 2], _buttons[1, 1], _buttons[2, 0]))
        {
            return true;
        }

        return false;
    }
}
