using OnlineChess.Behaviours;
using UnityEngine;

namespace OnlineChess.Cells
{
    public sealed class Cell : BaseObject, ICell
    {
        [SerializeField] private Renderer _renderCell;
        [SerializeField] private Renderer _renderSign;

        public Renderer RenderCell => _renderCell;
        public Renderer RenderSign => _renderSign;
        public Vector3 Position => transform.position;
    }
}