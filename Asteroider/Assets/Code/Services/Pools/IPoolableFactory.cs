using UnityEngine;

namespace Code.Services.Pools
{
    public interface IPoolableFactory<T> where T : MonoBehaviour, IPoolable<T>
    {
        int PrefabId { get; }
        T Create();
    }
}