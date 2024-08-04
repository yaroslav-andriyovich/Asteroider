using System.Collections;
using Code.Entities.Enemy;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.ObjectEmitting
{
    public class EnemyShipEmitter : MonoBehaviour
    {
        [SerializeField] private EnemyShip[] _enemyShipPrefabs;

        [SerializeField] private MinMaxFloat _interval;
        [SerializeField] private MinMaxFloat _speed;
        [SerializeField] private MinMaxFloat _deviationAngle;
        
        private ObjectEmittingZone _emittingZone;
        private PoolService _poolService;
        private MonoPool<EnemyShip>[] _enemyShipPools;

        private void Start()
        {
            _enemyShipPools = new MonoPool<EnemyShip>[_enemyShipPrefabs.Length];

            for (int i = 0; i < _enemyShipPrefabs.Length; i++)
                _enemyShipPools[i] = _poolService.GetPool(_enemyShipPrefabs[i]);
            
            StartCoroutine(EmittingRoutine());
        }

        [Inject]
        public void Construct(ObjectEmittingZone emittingZone, PoolService poolService)
        {
            _emittingZone = emittingZone;
            _poolService = poolService;
        }
        
        private IEnumerator EmittingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetInterval());

                EnemyShip spawnedObject = GetRandomEnemy();
                IEmittable component = spawnedObject.GetComponent<IEmittable>();

                SetPosition(spawnedObject);
                ApplyVelocity(component);
                
                component.Emit();
            }
        }

        private float GetInterval() => 
            Random.Range(_interval.min, _interval.max);

        private EnemyShip GetRandomEnemy() => 
            _enemyShipPools[Random.Range(0, _enemyShipPools.Length)].Get();

        private void SetPosition(EnemyShip enemyShip) => 
            enemyShip.transform.position = _emittingZone.GetRandomPosition();

        private void ApplyVelocity(IEmittable component)
        {
            float deviationAngle = Random.Range(_deviationAngle.min, _deviationAngle.max);
            float speed = -Random.Range(_speed.min, _speed.max);
            
            component.Rigidbody.velocity = new Vector3(deviationAngle, 0, speed);
        }
    }
}