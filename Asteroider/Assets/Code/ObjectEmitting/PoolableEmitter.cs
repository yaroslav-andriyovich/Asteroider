using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.ObjectEmitting
{
    public class PoolableEmitter<T> : Emitter<T> where T : MonoBehaviour, IEmittable, IPoolable<T>
    {
        private PoolService _poolService;
        private MonoPool<T>[] _objectPools;

        protected override void Start()
        {
            _objectPools = new MonoPool<T>[_objectPrefabs.Length];

            for (int i = 0; i < _objectPrefabs.Length; i++)
                _objectPools[i] = _poolService.GetPool(_objectPrefabs[i]);
            
            StartCoroutine(EmittingRoutine());
        }

        [Inject]
        public void Construct(PoolService poolService) => 
            _poolService = poolService;

        protected override T GetRandomObject() => 
            _objectPools[Random.Range(0, _objectPools.Length)].Get();
    }
}