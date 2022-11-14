using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IFigure
    {
        public FigureType CurrentType { get; }
        public Transform Transform { get; }
        public void UpdateFigure(FigureType type);
        public void SetActive(bool value);
        public void SetPosition(Vector3 position);
        public void SetLayer(int layer);
    }
}