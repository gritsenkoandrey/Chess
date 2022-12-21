using OnlineChess.UI;
using UnityEngine;

namespace OnlineChess.Services.Data
{
    [CreateAssetMenu(fileName = "UIAssetData", menuName = "Data/UIAssetData", order = 0)]
    public sealed class UIAssetData : ScriptableObject
    {
        [SerializeField] private UIMediator _uiMediator;

        public UIMediator UIMediator => _uiMediator;
    }
}