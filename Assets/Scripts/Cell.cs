using UnityEngine;

public sealed class Cell : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Renderer _rendererTwo;

    public Renderer Renderer => _renderer;
    public Renderer RendererTwo => _rendererTwo;
}