using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Pools
{
    public class PoolableFactory<T> : IPoolableFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        protected readonly IObjectResolver _objectResolver;
        protected readonly T _prefab;

        public PoolableFactory(IObjectResolver objectResolver, T prefab)
        {
            _objectResolver = objectResolver;
            _prefab = prefab;
        }

        public virtual T Create()
        {
            T poolable = _objectResolver.Instantiate(_prefab);
            
            return poolable;
        }
    }
}