using System.Collections;
using System.Collections.Generic;
using Code.Configs;
using Code.Entities;
using Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Code
{
    public class ObjectEmitter : MonoBehaviour
    {
        [SerializeField] private List<EmittableConfig> _emittableConfigs;

        private float _dividedLocalScaleX;
        private IObjectResolver _container;

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

        [Inject]
        public void Construct(IObjectResolver container) => 
            _container = container;

        private void Resize()
        {
            Vector3 scale = transform.localScale;
            
            scale.x = CameraUtils.GetStretchedSizeRelative().x;
            transform.localScale = scale;
        }

        private IEnumerator EmitCoroutine(EmittableConfig config)
        {
            while (true)
            {
                yield return new WaitForSeconds(GetNextLaunchTime(config));

                GameObject spawnedObject = _container.Instantiate(GetPrefabToSpawn(config), GetSpawnPosition(), Quaternion.identity);
                IEmittable component = spawnedObject.GetComponent<IEmittable>();
                
                ApplyAngularVelocity(config, component);
                ApplyVelocity(config, component);
            }
        }

        private float GetNextLaunchTime(EmittableConfig config) => 
            Random.Range(config.delay.min, config.delay.max);

        private GameObject GetPrefabToSpawn(EmittableConfig config)
        {
            int prefabsNumber = config.prefabs.Length;
            GameObject prefab = config.prefabs[Random.Range(0, prefabsNumber)];
            
            return prefab;
        }

        private Vector3 GetSpawnPosition() => 
            new Vector3(Random.Range(-_dividedLocalScaleX, _dividedLocalScaleX), 0f, transform.position.z);

        private void ApplyAngularVelocity(EmittableConfig config, IEmittable component) => 
            component.Rigidbody.angularVelocity = Random.insideUnitSphere * Random.Range(config.rotationSpeed.min, config.rotationSpeed.max);

        private void ApplyVelocity(EmittableConfig config, IEmittable component)
        {
            float deviationAngle = Random.Range(config.deviationAngle.min, config.deviationAngle.max);
            float speed = -Random.Range(config.speed.min, config.speed.max);
            
            component.Rigidbody.velocity = new Vector3(deviationAngle, 0, speed);
        }
    }
}