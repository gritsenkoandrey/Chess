using Behaviours;
using Data;
using UnityEngine;
using GameBoard = GameBoardBase.GameBoard;

namespace Factory
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAssetData _assetData;
        
        public GameFactory(IAssetData assetData) => _assetData = assetData;

        public Figures CreateFigure(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Object.Instantiate(_assetData.GameAssetData.Figures, pos, rot, parent);
        }

        public Cell CreateCell(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Object.Instantiate(_assetData.GameAssetData.Cell, pos, rot, parent);
        }

        public GameBoard CreateGameBoard()
        {
            return Object.Instantiate(_assetData.GameAssetData.GameBoard);
        }

        public GameCamera CreateGameCamera()
        {
            return Object.Instantiate(_assetData.GameAssetData.GameCamera);
        }
    }
}