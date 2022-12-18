using UnityEngine;

namespace OnlineChess.Scripts.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data", order = 0)]
    public sealed class AssetData : ScriptableObject, IAssetData
    {
        [SerializeField] private GameAssetData _gameAssetData;
        [SerializeField] private UIAssetData _uiAssetData;

        public GameAssetData GameAssetData => _gameAssetData;
        public UIAssetData UIAssetData => _uiAssetData;
    }
}