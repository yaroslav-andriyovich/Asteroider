using System;

namespace Code.Pools
{
    public interface IPoolable
    {
        void Release();
    }
    
    public interface IPoolable<T> : IPoolable
    {
        void Initialize(Action<T> returnAction);
    }
}