using OnlineChess.Services.Data;
using UnityEngine;

namespace OnlineChess.Utils
{
    public static class CustomResources
    {
        private const string DATA = "Data/Data";

        public static IAssetData Load<T>() where T : IAssetData
        {
            return Resources.Load(DATA, typeof(T)) as IAssetData;;
        }
    }
}