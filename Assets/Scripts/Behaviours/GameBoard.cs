using System.Collections.Generic;
using ChessRules;
using Enums;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Behaviours
{
    using Color = UnityEngine.Color;

    public sealed class GameBoard : MonoBehaviour
    {
        [SerializeField] private Figures _figure;
        [SerializeField] private Cell _cell;

        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _figuresRoot;
    
        private readonly Dictionary<string, ICell> _cells = new (64);
        private readonly Dictionary<string, IFigure> _figures = new (64);
        private readonly Dictionary<FigureType, IFigure> _promotions = new(8);

        private IDragAndDrop _dragAndDrop;

        private Chess _chess;
    
        private readonly Color _black = Color.black;
        private readonly Color _white = Color.white;
        private readonly Color _green = Color.green;
        private readonly Color _red = Color.red;

        private string _onTransformationMove = "";

        private void Awake()
        {
            _dragAndDrop = new DragAndDrop(DropFigure, PickFigure, PromotionFigure);

            _chess = new Chess();
        }

        private void Start()
        {
            InitGameBoard();
            UpdateFigures();
            MarkCellsFrom();
            InstantiatePromotions();
        }

        private void Update()
        {
            _dragAndDrop.Action();
        }

        private void InitGameBoard()
        {
            for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                string key = $"{x}{y}";

                _cells[key] = InstantiateCell(x, y);
                _figures[key] = InstantiateFigures(FigureType.None, x, y);
            }
        }

        private void InstantiatePromotions()
        {
            _promotions[FigureType.WhiteQueen] = InstantiateFigures(FigureType.WhiteQueen, 2, 9);
            _promotions[FigureType.WhiteRock] = InstantiateFigures(FigureType.WhiteRock, 3, 9);
            _promotions[FigureType.WhiteBishop] = InstantiateFigures(FigureType.WhiteBishop, 4, 9);
            _promotions[FigureType.WhiteKnight] = InstantiateFigures(FigureType.WhiteKnight, 5, 9);

            _promotions[FigureType.BlackQueen] = InstantiateFigures(FigureType.BlackQueen, 2, -2);
            _promotions[FigureType.BlackRock] = InstantiateFigures(FigureType.BlackRock, 3, -2);
            _promotions[FigureType.BlackBishop] = InstantiateFigures(FigureType.BlackBishop, 4, -2);
            _promotions[FigureType.BlackKnight] = InstantiateFigures(FigureType.BlackKnight, 5, -2);
            
            ShowPromotionsFigures();
        }
        private void ShowPromotionsFigures(FigureType type = FigureType.None)
        {
            foreach (IFigure figures in _promotions.Values)
            {
                figures.SetActive(false);
                figures.SetLayer(Layers.Promotions);
            }

            if (type == FigureType.WhitePawn)
            {
                _promotions[FigureType.WhiteQueen].SetActive(true);
                _promotions[FigureType.WhiteRock].SetActive(true);
                _promotions[FigureType.WhiteBishop].SetActive(true);
                _promotions[FigureType.WhiteKnight].SetActive(true);
            }
            else if (type == FigureType.BlackPawn)
            {
                _promotions[FigureType.BlackQueen].SetActive(true);
                _promotions[FigureType.BlackRock].SetActive(true);
                _promotions[FigureType.BlackBishop].SetActive(true);
                _promotions[FigureType.BlackKnight].SetActive(true);
            }
        }

        private ICell InstantiateCell(int x, int y)
        {
            ICell cell = Instantiate(_cell, new Vector3(x, 0f, y), Quaternion.identity, _cellsRoot);
            
            cell.RenderCell.material.color = (x + y) % 2 == 0 ? _black : _white;

            return cell;
        }
        private IFigure InstantiateFigures(FigureType type, int x, int y)
        {
            IFigure figures = Instantiate(_figure, new Vector3(x, 0f, y), Quaternion.identity, _figuresRoot);
            
            figures.UpdateFigure(type);

            return figures;
        }

        private void UpdateFigures()
        {
            for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                string key = $"{x}{y}";

                FigureType type = (FigureType)_chess.GetFigure(x, y);

                _figures[key].SetPosition(_cells[key].Position);

                if (_figures[key].CurrentType == type)
                {
                    continue;
                }

                _figures[key].UpdateFigure(type);
            }
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

            _onTransformationMove = "";
                
            UpdateFigures();
            MarkCellsFrom();
            ShowPromotionsFigures();

            foreach (IFigure figures in _figures.Values)
            {
                figures.UpdateFigure(figures.CurrentType);
            }
        }

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
                    _cells[key].RenderSign.material.color = (x + y) % 2 == 0 ? _black : _white;
                    break;
                case CellMarkType.From:
                    _cells[key].RenderSign.material.color = _green;
                    break;
                case CellMarkType.To:
                    _cells[key].RenderSign.material.color = _red;
                    break;
            }
        }
    }
}