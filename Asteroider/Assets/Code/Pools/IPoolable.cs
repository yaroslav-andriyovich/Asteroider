using System;

namespace Code.Pools
{
    public interface IPoolable<T>
    {
        void Initialize(Action<T> returnAction);
    }
}