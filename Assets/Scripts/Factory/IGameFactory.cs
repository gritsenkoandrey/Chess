using System.Collections.Generic;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.GameBoardBase;
using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Interfaces;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.Factory
{
    public interface IGameFactory : IService
    {
        public IGameBoard GameBoard { get; }
        public IGameCamera GameCamera { get; }

        public Figures CreateFigure(Vector3 pos, Quaternion rot, Transform parent);
        public Cell CreateCell(Vector3 pos, Quaternion rot, Transform parent);
        public GameBoard CreateGameBoard();
        public GameCamera CreateGameCamera();
        
        public List<IProgressReader> ProgressReaders { get; }
        public List<IProgressWriter> ProgressWriters { get; }

        public void Cleanup();
    }
}