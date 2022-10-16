using UnityEngine;

namespace Interfaces
{
    public interface ICell
    {
        public Vector3 Position { get; }
        public Renderer RenderCell { get; }
        public Renderer RenderSign { get; }
    }
}