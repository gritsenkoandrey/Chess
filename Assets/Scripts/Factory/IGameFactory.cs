using Behaviours;
using UnityEngine;
using GameBoard = GameBoardBase.GameBoard;

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