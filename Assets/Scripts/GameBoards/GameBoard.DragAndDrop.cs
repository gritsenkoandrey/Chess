using OnlineChess.Scripts.Enums;
using OnlineChess.Scripts.Extensions;
using OnlineChess.Scripts.Figures;
using OnlineChess.Scripts.Utils;
using UnityEngine;

namespace OnlineChess.Scripts.GameBoards
{
    public partial class GameBoard
    {
        private void DropFigure(Vector3 from, Vector3 to)
        {
            string e2 = from.VectorToCell();
            string e4 = to.VectorToCell();
            string figure = _chess.GetFigure((int)from.x, (int)from.z).ToString();
            string move = $"{figure}{e2}{e4}";
            
            if ((figure == "P" && e4[1] == '8') || (figure == "p" && e4[1] == '1'))
            {
                if (_chess.Move(move) != _chess)
                {
                    FigureType type = (FigureType)_chess.GetFigure((int)from.x, (int)from.z);

                    ShowPromotionsFigures(type);

                    foreach (IFigure figures in _figures.Values)
                    {
                        figures.SetLayer(Layers.Default);
                    }
                
                    UnmarkCells();
                    
                    _onTransformationMove = move;
                    
                    _dragAndDrop.ChangeState(State.Promotion);
                
                    return;
                }
            }

            if (_chess.Move(move) != _chess)
            {
                ChangeTurn.Invoke();
            }

            _chess = _chess.Move(move);

            UpdateFigures();
            MarkCellsFrom();
            
            UpdateChess.Invoke(_chess);
        }
        
        private void PickFigure(Vector3 from)
        {
            MarkCellsTo(from.VectorToCell());
        }
        
        private void PromotionFigure(Vector3 from)
        {
            int x = (int)from.x;

            if (_onTransformationMove[0] == 'P')
            {
                if (x == 2) _onTransformationMove += "Q";
                else if (x == 3) _onTransformationMove += "R";
                else if (x == 4) _onTransformationMove += "B";
                else if (x == 5) _onTransformationMove += "N";
            }
            else if (_onTransformationMove[0] == 'p')
            {
                if (x == 2) _onTransformationMove += "q";
                else if (x == 3) _onTransformationMove += "r";
                else if (x == 4) _onTransformationMove += "b";
                else if (x == 5) _onTransformationMove += "n";
            }

            ChangeTurn.Invoke();
            
            _chess = _chess.Move(_onTransformationMove);

            _onTransformationMove = "";
                
            UpdateFigures();
            MarkCellsFrom();
            ShowPromotionsFigures();

            foreach (IFigure figures in _figures.Values)
            {
                figures.UpdateFigure(figures.CurrentType);
            }
            
            UpdateChess.Invoke(_chess);
        }
    }
}