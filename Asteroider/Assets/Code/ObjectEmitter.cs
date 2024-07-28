using System.Collections;
using System.Collections.Generic;
using Code.Configs;
using Code.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class ObjectEmitter : MonoBehaviour
    {
        [SerializeField] private ForwardDirection _forwardDirection;
        [SerializeField] private List<EmittableConfig> _emittableConfigs;

        private float _dividedLocalScaleX;
        
        private void Awake()
        {
            Resize();
            _dividedLocalScaleX = transform.localScale.x / 2;
        }

        private void Start()
        {
            foreach (EmittableConfig config in _emittableConfigs)
                StartCoroutine(EmitCoroutine(config));
        }

        private void Resize()
        {
            Vector3 scale = transform.localScale;
            
            scale.x = Utils.GetStretchedSizeRelativeToCamera().x;
            transform.localScale = scale;
        }

        private IEnumerator EmitCoroutine(EmittableConfig config)
        {
            while (true)
            {
                yield return new WaitForSeconds(GetNextLaunchTime(config));

                GameObject spawnedObject = Instantiate(GetPrefabToSpawn(config), GetSpawnPosition(), Quaternion.identity);
                
                spawnedObject.GetComponent<IEmittable>().Emit((float)_forwardDirection);
            }
        }

        private float GetNextLaunchTime(EmittableConfig config) => 
            Random.Range(config.minDelay, config.maxDelay);

        private GameObject GetPrefabToSpawn(EmittableConfig config)
        {
            int prefabsNumber = config.prefabs.Length;
            GameObject prefab = config.prefabs[Random.Range(0, prefabsNumber)];
            
            return prefab;
        }

        private Vector3 GetSpawnPosition() => 
            new Vector3(Random.Range(-_dividedLocalScaleX, _dividedLocalScaleX), 0f, transform.position.z);

        private enum ForwardDirection
        {
            Forward = 1,
            Backward = -1
        }
    }
}