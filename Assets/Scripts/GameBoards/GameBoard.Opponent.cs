using System.Collections;
using OnlineChess.Scripts.Enums;
using OnlineChess.Scripts.Extensions;
using OnlineChess.Scripts.Figures;
using OnlineChess.Scripts.Utils;
using UnityEngine;

namespace OnlineChess.Scripts.GameBoards
{
    public partial class GameBoard
    {
        private IEnumerator OpponentMoveCoroutine()
        {
            _dragAndDrop.ChangeState(State.Opponent);
            
            string move = _chess.YieldValidMoves().PickRandom();
            
            int xFrom = move[1] - 'a';
            int zFrom = move[2] - '1';
            
            int xTo = move[3] - 'a';
            int zTo = move[4] - '1';
                        
            UnmarkCells();
            
            ShowCell(xFrom, zFrom, CellMarkType.From);

            yield return new WaitForSeconds(0.25f);
            
            ShowCell(xFrom, zFrom);
            ShowCell(xTo, zTo, CellMarkType.To);

            string key = $"{xFrom}{zFrom}";

            IFigure figure = _figures[key];

            Vector3 from = new Vector3(xFrom, 0f, zFrom);
            Vector3 to = new Vector3(xTo, 0f, zTo);
            Vector3 center = ((from + to) * 0.5f).AddY(2.5f);

            int points = 35;

            for (int i = 1; i <= points; i++)
            {
                float time = i / (float)points;

                figure.Transform.position = BezierCurves.Quadratic(from, center, to, time);
                
                yield return null;
            }

            ChangeTurn.Invoke();
            
            _chess = _chess.Move(move);

            UpdateChess.Invoke(_chess);

            UpdateFigures();
            MarkCellsFrom();
            
            _dragAndDrop.ChangeState(State.Player);
        }
    }
}