using Enums;
using Extensions;
using Interfaces;
using UnityEngine;
using Utils;

namespace GameBoards
{
    public partial class GameBoard
    {
        private IDragAndDrop _dragAndDrop;

        private string _onTransformationMove = "";

        private void DragAndDrop()
        {
            _dragAndDrop = new DragAndDrop(DropFigure, PickFigure, PromotionFigure);
        }

        private void Execute()
        {
            _dragAndDrop.Action();
        }

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

            _chess = _chess.Move(move);
            
            UpdateChess.Invoke(_chess);

            UpdateFigures();
            MarkCellsFrom();
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

            _chess = _chess.Move(_onTransformationMove);
            
            UpdateChess.Invoke(_chess);

            _onTransformationMove = "";
                
            UpdateFigures();
            MarkCellsFrom();
            ShowPromotionsFigures();

            foreach (IFigure figures in _figures.Values)
            {
                figures.UpdateFigure(figures.CurrentType);
            }
        }
    }
}