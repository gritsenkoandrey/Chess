using System.Collections.Generic;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.Data;
using OnlineChess.Scripts.GameBoardBase;
using OnlineChess.Scripts.Interfaces;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.Factory
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAssetData _assetData;
        
        public IGameBoard GameBoard { get; private set; }
        public IGameCamera GameCamera { get; private set; }

        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        public GameFactory(IAssetData assetData)
        {
            _assetData = assetData;
        }

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
            GameBoard gameBoard = Object.Instantiate(_assetData.GameAssetData.GameBoard);

            GameBoard = gameBoard;
            
            Registered(gameBoard);
            
            return gameBoard;
        }

        public GameCamera CreateGameCamera()
        {
            GameCamera gameCamera =  Object.Instantiate(_assetData.GameAssetData.GameCamera);

            GameCamera = gameCamera;

            return gameCamera;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Registered(IProgress progress)
        {
            if (progress is IProgressWriter writer)
            {
                ProgressWriters.Add(writer);
            }

            if (progress is IProgressReader reader)
            {
                ProgressReaders.Add(reader);
            }
        }
    }
}