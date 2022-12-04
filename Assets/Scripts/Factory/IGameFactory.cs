using Behaviours;
using GameBoardBase;
using UnityEngine;

namespace Factory
{
    public interface IGameFactory
    {
        public Figures CreateFigure(Vector3 pos, Quaternion rot, Transform parent);
        public Cell CreateCell(Vector3 pos, Quaternion rot, Transform parent);
        public GameBoard CreateGameBoard();
        public GameCamera CreateGameCamera();
    }
}