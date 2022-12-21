using System.Collections.Generic;
using OnlineChess.Cameras;
using OnlineChess.Cells;
using OnlineChess.Figures;
using OnlineChess.GameBoards;
using OnlineChess.Services.Data;
using OnlineChess.Services.PersistentProgress;
using OnlineChess.UI;
using UnityEngine;

namespace OnlineChess.Services.Factories
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