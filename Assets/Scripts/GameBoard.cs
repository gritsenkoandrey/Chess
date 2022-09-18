using System.Collections.Generic;
using UnityEngine;
using ChessRules;
using Color = UnityEngine.Color;

public sealed class GameBoard : MonoBehaviour
{
    [SerializeField] private Figures _figure;
    [SerializeField] private Cell _cell;

    [SerializeField] private Transform _cellsRoot;
    [SerializeField] private Transform _figuresRoot;
    
    private readonly Dictionary<string, Cell> _cells = new (64);
    private readonly Dictionary<string, Figures> _figures = new (64);

    private DragAndDrop _dragAndDrop;

    private Chess _chess;
    
    private readonly Color _black = Color.black;
    private readonly Color _white = Color.white;
    private readonly Color _green = Color.green;
    private readonly Color _yellow = Color.yellow;

    private void Awake()
    {
        _dragAndDrop = new DragAndDrop(DropFigure, PickFigure);

        _chess = new Chess();
    }

    private void Start()
    {
        InitGameBoard();
        ShowFigures();
        MarkCellsFrom();
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

            Vector3 position = new Vector3(x, 0f, y);

            _cells[key] = Instantiate(_cell, position, Quaternion.identity, _cellsRoot);
            _cells[key].Renderer.material.color = (x + y) % 2 == 0 ? _black : _white;
            
            _figures[key] = Instantiate(_figure, position, Quaternion.identity, _figuresRoot);
            _figures[key].ShowFigure(FigureType.None);
            _figures[key].name = "None";
        }
    }

    private void ShowFigures()
    {
        for (int y = 0; y < 8; y++)
        for (int x = 0; x < 8; x++)
        {
            string key = $"{x}{y}";

            FigureType type = (FigureType)_chess.GetFigure(x, y);

            _figures[key].transform.position = _cells[key].transform.position;

            if (_figures[key].CurrentType == type)
            {
                continue;
            }

            _figures[key].ShowFigure(type);

            _figures[key].name = type.ToString();
        }
    }
    
    private void DropFigure(Vector3 from, Vector3 to)
    {
        string e2 = from.VectorToCell();
        string e4 = to.VectorToCell();
        string figure = _chess.GetFigure((int)from.x, (int)from.z).ToString();
        string move = $"{figure}{e2}{e4}";

        _chess = _chess.Move(move);
        
        ShowFigures();
        MarkCellsFrom();
    }

    private void PickFigure(Vector3 from)
    {
        MarkCellsTo(from.VectorToCell());
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
                _cells[key].RendererTwo.material.color = (x + y) % 2 == 0 ? _black : _white;
                break;
            case CellMarkType.From:
                _cells[key].RendererTwo.material.color = _green;
                break;
            case CellMarkType.To:
                _cells[key].RendererTwo.material.color = _yellow;
                break;
        }
    }
}