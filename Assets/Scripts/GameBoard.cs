using System.Collections.Generic;
using UnityEngine;
using ChessRules;

public sealed class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject _cellBlack;
    [SerializeField] private GameObject _cellWhite;
    [SerializeField] private Figures _figure;

    [SerializeField] private Transform _cellsRoot;
    [SerializeField] private Transform _figuresRoot;
    
    private readonly Dictionary<string, GameObject> _cells = new (64);
    private readonly Dictionary<string, Figures> _figures = new (64);

    private DragAndDrop _dragAndDrop;

    private Chess _chess;

    private void Awake()
    {
        _dragAndDrop = new DragAndDrop(DropFigure);

        _chess = new Chess();
    }

    private void Start()
    {
        InitGameBoard();
        ShowFigures();
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
            string key = "" + x + y;

            GameObject cell = (x + y) % 2 == 0 ? _cellBlack : _cellWhite;

            Vector3 position = new Vector3(x, 0f, y);

            _cells[key] = Instantiate(cell, position, Quaternion.identity, _cellsRoot);
            
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
            string key = "" + x + y;

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
        string e2 = VectorToCell(from);
        string e4 = VectorToCell(to);
        string figure = _chess.GetFigure((int)from.x, (int)from.z).ToString();
        string move = $"{figure}{e2}{e4}";

        _chess = _chess.Move(move);
        
        ShowFigures();
    }

    private string VectorToCell(Vector3 vector)
    {
        int x = (int)vector.x;
        int y = (int)vector.z;

        // if (x is >= 0 and < 8 && y is >= 0 and < 8)
        // {
        //     return (char)('a' + x) + (y + 1).ToString();
        // }
        //return null;
        
        return (char)('a' + x) + (y + 1).ToString();
    }
}