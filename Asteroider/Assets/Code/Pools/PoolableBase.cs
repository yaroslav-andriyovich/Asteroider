using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Pools
{
    public abstract class PoolableBase<T> : MonoBehaviour, IPoolable<T> where T : PoolableBase<T>
    {
        private Action<T> _returnToPool;

        void IPoolable<T>.Initialize(Action<T> returnAction) => 
            _returnToPool = returnAction;

        protected void Release() => 
            _returnToPool((T)this);

        protected void Release(float delay) => 
            DOVirtual.DelayedCall(delay, Release);
        
        protected void Release(float delay, Action delayedAction) => 
            DOVirtual.DelayedCall(delay, () =>
            {
                delayedAction?.Invoke();
                Release();
            });
    }
}