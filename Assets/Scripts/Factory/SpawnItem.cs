using Behaviours;
using Interfaces;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "SpawnItem", menuName = "Factory/Item", order = 0)]
    public sealed class SpawnItem : SpawnFactory
    {
        [SerializeField] private Figures _figures;
        [SerializeField] private Cell _cell;

        public IFigure GetFigure(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Get(_figures, pos, rot, parent);
        }
        
        public ICell GetCell(Vector3 pos, Quaternion rot, Transform parent)
        {
            return Get(_cell, pos, rot, parent);
        }
        
        private T Get<T>(T prefab, Vector3 pos, Quaternion rot, Transform parent) where T : BaseObject
        {
            return Create(prefab, pos, rot, parent);
        }
    }
}