using System;
using ChessRules;

namespace OnlineChess.GameBoards
{
    public interface IGameBoard
    {
        public void StartGame();
        public void RestartGame();
        public void OpponentMove();
        public Action<Chess> UpdateChess { get; set; }
        public Action ChangeTurn { get; set; }
    }
}