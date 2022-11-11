using Behaviours;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "Data/Prefab", order = 0)]
    public sealed class PrefabsData : ScriptableObject
    {
        [SerializeField] private Cell _cell;
        [SerializeField] private Figures _figures;

        public Cell Cell => _cell;
        public Figures Figures => _figures;
    }
}