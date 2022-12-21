using System.Collections.Generic;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.Cameras;
using OnlineChess.Scripts.Cells;
using OnlineChess.Scripts.Figures;
using OnlineChess.Scripts.GameBoards;
using OnlineChess.Scripts.Services.Data;
using OnlineChess.Scripts.Services.PersistentProgress;
using OnlineChess.Scripts.UI;
using UnityEngine;

namespace OnlineChess.Scripts.Services.Factories
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

        public IFigure CreateFigure(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Object.Instantiate(_assetData.GameAssetData.Figures, pos, rot, parent);
        }

        public ICell CreateCell(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Object.Instantiate(_assetData.GameAssetData.Cell, pos, rot, parent);
        }

        public IGameBoard CreateGameBoard()
        {
            GameBoard = Object.Instantiate(_assetData.GameAssetData.GameBoard);
            
            Registered((GameBoard)GameBoard);
            
            return GameBoard;
        }

        public IGameCamera CreateGameCamera()
        {
            return GameCamera = Object.Instantiate(_assetData.GameAssetData.GameCamera);
        }
        
        public UIMediator CreateUIMediator()
        {
            UIMediator uiMediator = Object.Instantiate(_assetData.UIAssetData.UIMediator);
            
            Registered(uiMediator);

            return uiMediator;
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