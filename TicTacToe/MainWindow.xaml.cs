using System.ComponentModel;
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
using TicTacToe.Controls;
using TicTacToe.Enums;

namespace TicTacToe;
public partial class MainWindow : Window
{
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private string endGameState;

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;
    }

    

    private void NewGame_Click(object sender, RoutedEventArgs e)
    {
        GameType gameType;
        Player1Type player1Type;
        Player2Type player2Type;

        if (sender == Btn_PvP)
        {
            gameType = GameType.PvP;
            player1Type = Player1Type.Player1;
            player2Type = Player2Type.Player2;
        }
        else if (sender == Btn_PvC)
        {
            gameType = GameType.PvC;
            player1Type = Player1Type.Player;
            player2Type = Player2Type.Computer;
        }
        else if (sender == Btn_CvC)
        {
            gameType = GameType.CvC;
            player1Type = Player1Type.Computer1;
            player2Type = Player2Type.Computer2;
        }
        else
        {
            return;
        }

        MyBoard.StartNewGame(gameType, player1Type, player2Type);
    }

    private void Button_Exit(object sender, RoutedEventArgs e)
    {
        MessageBoxResult resultPlayer = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButton.YesNo);

        if (resultPlayer == MessageBoxResult.No)
        {
            return;
        }
        else
        {
            var window = Window.GetWindow(this) as MainWindow;
            window.Close();
        }
        
    }

    private void Button_info(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Rules: \neach player in turn fills a square with an X or an O according to his symbol" +
            "\nTo win the player must fill 3 squares in a row with his symbol in a row, column or diagonal" +
            "\n\nThe menu has 3 buttons for selecting game modes \n1. Player against player \n2. Player vs. computer \n3. Computer versus computer" +
            "\n\nAt the end of each game, a window will open with the winner announced" +
            "\nTo start a new game, click \"YES\" or click the Mode Game button in the menu");
    }

    private void Btn_Score(object sender, RoutedEventArgs e)
    {
        MessageBoxResult resultPlayer = MessageBox.Show("Are you sure you want to reset the score?", "", MessageBoxButton.YesNo);

        if (resultPlayer == MessageBoxResult.No)
        {
            return;
        }
        else
        {
            MyBoard.PlayerOneScore = 0;
            MyBoard.PlayerTwoScore = 0;
        }
        
    }
}