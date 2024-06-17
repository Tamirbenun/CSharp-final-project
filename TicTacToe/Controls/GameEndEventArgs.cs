using TicTacToe.Enums;

namespace TicTacToe.Controls;

public class GameEndEventArgs : EventArgs
{
    public GameEndEventArgs(GameResult result)
    {
        GameResult = result;

    }

    public GameResult GameResult { get; set; }
}