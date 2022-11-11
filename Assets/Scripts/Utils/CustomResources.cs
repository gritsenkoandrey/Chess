using Interfaces;
using UnityEngine;

namespace Utils
{
    public static class CustomResources
    {
        private const string DATA = "Data/Data";

        public static IData LoadData<T>() where T : IData
        {
            return Resources.Load(DATA, typeof(T)) as IData;;
        }
    }
}