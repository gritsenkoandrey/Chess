using System.Collections.Generic;
using OnlineChess.Scripts.Cameras;
using OnlineChess.Scripts.Cells;
using OnlineChess.Scripts.Figures;
using OnlineChess.Scripts.GameBoards;
using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Services.PersistentProgress;
using OnlineChess.Scripts.UI;
using UnityEngine;

namespace OnlineChess.Scripts.Services.Factories
{
    public interface IGameFactory : IService
    {
        public IGameBoard GameBoard { get; }
        public IGameCamera GameCamera { get; }

        public IFigure CreateFigure(Vector3 pos, Quaternion rot, Transform parent);
        public ICell CreateCell(Vector3 pos, Quaternion rot, Transform parent);
        public IGameBoard CreateGameBoard();
        public IGameCamera CreateGameCamera();
        public UIMediator CreateUIMediator();
        
        public List<IProgressReader> ProgressReaders { get; }
        public List<IProgressWriter> ProgressWriters { get; }

        public void Cleanup();
    }
}