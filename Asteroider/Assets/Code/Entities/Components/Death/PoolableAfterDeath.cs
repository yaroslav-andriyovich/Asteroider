using Code.Services.Pools;

namespace Code.Entities.Components.Death
{
    public class PoolableAfterDeath : DestroyAfterDeath
    {
        private IPoolable _poolable;

        protected override void Awake()
        {
            base.Awake();

            _poolable = GetComponent<IPoolable>();
        }

        public override void ExecuteAfterDeath() =>
            _poolable.Release();
    }
}