using Enums;
using UnityEngine;

namespace GameBoards
{
    public partial class GameBoard
    {
        private void MarkCellsFrom()
        {
            UnmarkCells();
        
            foreach (string move in _chess.YieldValidMoves())
            {
                ShowCell(move[1] - 'a', move[2] - '1', CellMarkType.From);
            }
        }
        
        private void MarkCellsTo(string from)
        {
            UnmarkCells();
        
            foreach (string move in _chess.YieldValidMoves())
            {
                if (from == move.Substring(1, 2))
                {
                    ShowCell(move[3] - 'a', move[4] - '1', CellMarkType.To);
                }
            }
        }
        
        private void UnmarkCells()
        {
            for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                ShowCell(x, y);
            }
        }
        
        private void ShowCell(int x, int y, CellMarkType markType = CellMarkType.Original)
        {
            string key = $"{x}{y}";

            switch (markType)
            {
                case CellMarkType.Original:
                    _cells[key].RenderSign.material.color = (x + y) % 2 == 0 ? Color.black : Color.white;
                    break;
                case CellMarkType.From:
                    _cells[key].RenderSign.material.color = Color.green;
                    break;
                case CellMarkType.To:
                    _cells[key].RenderSign.material.color = Color.red;
                    break;
            }
        }
    }
}