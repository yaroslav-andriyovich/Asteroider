using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Pools
{
    public class PoolService
    {
        private readonly Dictionary<Type, object> _pools;

        public PoolService() => 
            _pools = new Dictionary<Type, object>();

        public MonoPool<T> CreatePool<T>(IPoolableFactory<T> poolableFactory, int count, bool autoExpand = true) where T : MonoBehaviour, IPoolable<T>
        {
            if (_pools.ContainsKey(typeof(T)))
                throw new Exception($"Pool of type {typeof(T)} already created");
            
            MonoPool<T> pool = new MonoPool<T>(poolableFactory, count, autoExpand);
            
            _pools[typeof(T)] = pool;
            
            return pool;
        }

        public MonoPool<T> GetPool<T>() where T : MonoBehaviour, IPoolable<T>
        {
            if (_pools.TryGetValue(typeof(T), out object pool))
                return (MonoPool<T>)pool;

            throw new Exception($"No pool found for type {typeof(T)}");
        }
    }
}