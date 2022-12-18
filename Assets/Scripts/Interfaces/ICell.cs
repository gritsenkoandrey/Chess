using UnityEngine;

namespace OnlineChess.Scripts.Interfaces
{
    public interface ICell
    {
        public Vector3 Position { get; }
        public Renderer RenderCell { get; }
        public Renderer RenderSign { get; }
    }
}