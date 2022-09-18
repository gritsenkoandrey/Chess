using UnityEngine;

public sealed class Figure : MonoBehaviour
{
    [SerializeField] private FigureType _figureType;
    [SerializeField] private Renderer _renderer;

    public FigureType Type => _figureType;
    public Renderer Renderer => _renderer;
}