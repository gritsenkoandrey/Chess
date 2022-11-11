using Enums;
using Interfaces;
using UnityEngine;
using Utils;

namespace Behaviours
{
    public sealed class Figures : MonoBehaviour, IFigure
    {
        [SerializeField] private Figure[] _figuresPrefabs;
        public FigureType CurrentType { get; private set; } = FigureType.None;

        public void UpdateFigure(FigureType type)
        {
            CurrentType = type;

            gameObject.name = $"{type}";
            
            SetLayer(CurrentType == FigureType.None ? Layers.Default : Layers.Figure);
        
            for (int i = 0; i < _figuresPrefabs.Length; i++)
            {
                _figuresPrefabs[i].Renderer.enabled = _figuresPrefabs[i].Type == type;
            }
        }

        public void SetActive(bool value) => gameObject.SetActive(value);
        public void SetPosition(Vector3 position) => transform.position = position;
        public void SetLayer(int layer) => gameObject.layer = layer;
    }
}