using UnityEngine;

public sealed class Figures : MonoBehaviour
{
    [SerializeField] private Figure[] _figuresPrefabs;
    [SerializeField] private Collider _collider;
    public FigureType CurrentType { get; private set; } = FigureType.None;

    public void ShowFigure(FigureType type)
    {
        CurrentType = type;

        _collider.gameObject.layer = CurrentType == FigureType.None ? Layers.Default : Layers.Figure;
        
        for (int i = 0; i < _figuresPrefabs.Length; i++)
        {
            _figuresPrefabs[i].Renderer.enabled = _figuresPrefabs[i].Type == type;
        }
    }
}