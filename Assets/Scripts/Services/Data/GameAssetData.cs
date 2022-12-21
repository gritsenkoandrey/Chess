using OnlineChess.Scripts.Cameras;
using OnlineChess.Scripts.Cells;
using OnlineChess.Scripts.Figures;
using UnityEngine;
using GameBoard = OnlineChess.Scripts.GameBoards.GameBoard;

namespace OnlineChess.Scripts.Services.Data
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