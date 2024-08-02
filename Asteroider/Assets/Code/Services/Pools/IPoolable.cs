using System;

namespace Code.Services.Pools
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