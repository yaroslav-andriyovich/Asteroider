using Code.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "EmittableConfig", menuName = "ScriptableObjects/EmittableConfig", order = 1)]
    public class EmittableConfig : ScriptableObject
    {
        public GameObject[] prefabs;
        public float minDelay;
        public float maxDelay;

        private void OnValidate()
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i].GetComponent<IEmittable>() == null)
                {
                    Debug.LogError($"Prefab {prefabs[i].name} hasn't IEmittable interface implementation!");
                    prefabs[i] = null;
                }
            }
        }
    }
}