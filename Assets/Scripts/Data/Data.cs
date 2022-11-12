using Factory;
using Interfaces;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data", order = 0)]
    public class Data : ScriptableObject, IData
    {
        [SerializeField] private SpawnItem _spawnItem;

        public SpawnItem SpawnItem => _spawnItem;
    }
}