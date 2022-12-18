using System;

namespace OnlineChess.Scripts.BoardProgressData
{
    [Serializable]
    public sealed class BoardProgress
    {
        public string Fen;
        public int Turn;

        public BoardProgress()
        {
            Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            Turn = 1;
        }

        public BoardProgress(string fen, int turn)
        {
            Fen = fen;
            Turn = turn;
        }
    }
}