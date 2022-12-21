using UnityEngine;

namespace OnlineChess.Cells
{
    public interface ICell
    {
        public Vector3 Position { get; }
        public Renderer RenderCell { get; }
        public Renderer RenderSign { get; }
    }
}