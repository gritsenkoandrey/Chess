using Behaviours;
using UnityEngine;
using GameBoard = GameBoardBase.GameBoard;

namespace Data
{
    [CreateAssetMenu(fileName = "GameAssetData", menuName = "Data/GameAssetData", order = 0)]
    public sealed class GameAssetData : ScriptableObject
    {
        [SerializeField] private Figures _figures;
        [SerializeField] private Cell _cell;
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameCamera _gameCamera;

        public Figures Figures => _figures;
        public Cell Cell => _cell;
        public GameBoard GameBoard => _gameBoard;
        public GameCamera GameCamera => _gameCamera;
    }
}