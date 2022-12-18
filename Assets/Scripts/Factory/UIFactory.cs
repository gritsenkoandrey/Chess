using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.Data;
using UnityEngine;

namespace OnlineChess.Scripts.Factory
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAssetData _assetData;

        public UIFactory(IAssetData assetData) => _assetData = assetData;

        public UIMediator CreateUIMediator()
        {
            return Object.Instantiate(_assetData.UIAssetData.UIMediator);
        }
    }
}