using System.Collections.Generic;
using Code.Configs;
using UnityEngine;

namespace Code
{
    public class ObjectEmitter : MonoBehaviour
    {
        [SerializeField] private List<EmittableConfig> _emittableConfigs;
        
        private float _dividedLocalScaleX;
        private float[] _nextLaunchTimes;
        private float _nextAsteroidLaunchTime;
        
        private void Awake()
        {
            Resize();
            _dividedLocalScaleX = transform.localScale.x / 2;
            InitLaunchTimesArray();
        }

        private void Update()
        {
            for (int i = 0; i < _emittableConfigs.Count; i++)
            {
                if (Time.time > _nextLaunchTimes[i])
                {
                    EmittableConfig config = _emittableConfigs[i];
                    int prefabsLength = config.prefabs.Length;
                    GameObject obj = config.prefabs[Random.Range(0, prefabsLength)];
                    Vector3 position = new Vector3(Random.Range(-_dividedLocalScaleX, _dividedLocalScaleX), 0f, transform.position.z);
                    
                    Instantiate(obj, position, Quaternion.identity);
                    _nextLaunchTimes[i] = GetNextLaunchTime(config);
                }
            }
        }

        private void Resize()
        {
            Vector3 scale = transform.localScale;

            scale.x = Utils.GetStretchedSizeRelativeToCamera().x;

            transform.localScale = scale;
        }

        private void InitLaunchTimesArray()
        {
            _nextLaunchTimes = new float[_emittableConfigs.Count];

            for (int i = 0; i < _emittableConfigs.Count; i++)
                _nextLaunchTimes[i] = GetNextLaunchTime(_emittableConfigs[i]);
        }

        private float GetNextLaunchTime(EmittableConfig config) => 
            Time.time + Random.Range(config.minDelay, config.maxDelay);
    }
}