using UnityEngine;

namespace Code.Pools
{
    public interface IPoolableFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        T Create();
    }
}