using OnlineChess.Scripts.Behaviours;
using UnityEngine;

namespace OnlineChess.Scripts.Data
{
    [CreateAssetMenu(fileName = "UIAssetData", menuName = "Data/UIAssetData", order = 0)]
    public sealed class UIAssetData : ScriptableObject
    {
        [SerializeField] private UIMediator _uiMediator;

        public UIMediator UIMediator => _uiMediator;
    }
}