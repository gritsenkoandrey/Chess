using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.Enums;
using UnityEngine;

namespace OnlineChess.Scripts.Figures
{
    public sealed class Figure : BaseObject
    {
        [SerializeField] private FigureType _figureType;
        [SerializeField] private Renderer _renderer;

        public FigureType Type => _figureType;
        public Renderer Renderer => _renderer;
    }
}