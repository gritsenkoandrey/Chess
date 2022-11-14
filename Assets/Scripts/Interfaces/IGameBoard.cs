using System;
using ChessRules;

namespace Interfaces
{
    public interface IGameBoard
    {
        public void StartGame();
        public void RestartGame();
        public Action<Chess> UpdateChess { get; set; }
        public Action ChangeTurn { get; set; }
    }
}