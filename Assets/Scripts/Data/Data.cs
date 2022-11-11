using Interfaces;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data", order = 0)]
    public class Data : ScriptableObject, IData
    {
        [SerializeField] private PrefabsData _prefabsData;

        public PrefabsData PrefabsData => _prefabsData;
    }
}