using OnlineChess.Cameras;
using OnlineChess.Cells;
using OnlineChess.Figures;
using UnityEngine;
using GameBoard = OnlineChess.GameBoards.GameBoard;

namespace OnlineChess.Services.Data
{
    [CreateAssetMenu(fileName = "GameAssetData", menuName = "Data/GameAssetData", order = 0)]
    public sealed class GameAssetData : ScriptableObject
    {
        [SerializeField] private FigureMediator _figures;
        [SerializeField] private Cell _cell;
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameCamera _gameCamera;

        public FigureMediator Figures => _figures;
        public Cell Cell => _cell;
        public GameBoard GameBoard => _gameBoard;
        public GameCamera GameCamera => _gameCamera;
    }
}