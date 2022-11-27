using Behaviours;
using Data;
using UnityEngine;

namespace Factory
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