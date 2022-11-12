using UnityEngine;

namespace Factory
{
    public abstract class SpawnFactory : ScriptableObject
    {
        protected T Create<T>(T prefab, Vector3 pos, Quaternion rot, Transform parent) where T : Object
        {
            T instance = Instantiate(prefab, pos, rot, parent);
            
            return instance;
        }
    }
}