using Enums;
using Interfaces;
using UnityEngine;
using Utils;

namespace GameBoards
{
    public partial class GameBoard
    {
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _figuresRoot;
        
        private IData _data;

        private void Initialize()
        {
            _data = CustomResources.LoadData<IData>();
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
        
        private ICell InstantiateCell(int x, int y)
        {
            ICell cell = _data.SpawnItem.GetCell(new Vector3(x, 0f, y), Quaternion.identity, _cellsRoot);
            
            cell.RenderCell.material.color = (x + y) % 2 == 0 ? Color.black : Color.white;

            return cell;
        }
        
        private IFigure InstantiateFigures(FigureType type, int x, int y)
        {
            IFigure figure = _data.SpawnItem.GetFigure(new Vector3(x, 0f, y), Quaternion.identity, _figuresRoot);
            
            figure.UpdateFigure(type);

            return figure;
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
    }
}