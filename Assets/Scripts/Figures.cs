using UnityEngine;

public sealed class Figures : MonoBehaviour
{
    [SerializeField] private Figure[] _figuresPrefabs;
    
    public FigureType CurrentType { get; private set; }

    public void ShowFigure(FigureType type)
    {
        CurrentType = type;
        
        for (int i = 0; i < _figuresPrefabs.Length; i++)
        {
            _figuresPrefabs[i].Renderer.enabled = _figuresPrefabs[i].Type == type;
        }
    }
}