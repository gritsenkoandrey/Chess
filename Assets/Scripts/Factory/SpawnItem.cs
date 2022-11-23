using Behaviours;
using GameBoards;
using Interfaces;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "SpawnItem", menuName = "Factory/Item", order = 0)]
    public sealed class SpawnItem : SpawnFactory
    {
        [SerializeField] private Figures _figures;
        [SerializeField] private Cell _cell;
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameCamera _gameCamera;
        [SerializeField] private UIMediator _uiMediator;

        public IFigure GetFigure(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Get(_figures, pos, rot, parent);
        }
        
        public ICell GetCell(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Get(_cell, pos, rot, parent);
        }

        public IGameBoard GetGameBoard()
        {
            return Get(_gameBoard, Vector3.zero, Quaternion.identity, null);
        }

        public IGameCamera GetGameCamera()
        {
            return Get(_gameCamera, Vector3.zero, Quaternion.identity, null);
        }

        public UIMediator GetUIMediator()
        {
            return Get(_uiMediator, Vector3.zero, Quaternion.identity, null);
        }
        
        private T Get<T>(T prefab, Vector3 pos, Quaternion rot, Transform parent) where T : BaseObject
        {
            return Create(prefab, pos, rot, parent);
        }
    }
}