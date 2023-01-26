using System.Collections.Generic;
using OnlineChess.Cameras;
using OnlineChess.Cells;
using OnlineChess.Figures;
using OnlineChess.GameBoards;
using OnlineChess.Infrastructure.Services;
using OnlineChess.Services.PersistentProgress;
using OnlineChess.UI;
using UnityEngine;

namespace OnlineChess.Services.Factories
{
    public interface IGameFactory : IService
    {
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