using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Components
{
    public class LimitedScreenMotionProvider : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public ScreenLimitOperation Operation { get; private set; }

        public bool IsPoolable => PoolableRef != null;
        public IPoolable PoolableRef { get; private set; }
        
        private ScreenBoundaryLimitService _limitService;

        private void Awake()
        {
            if (TryGetComponent(out IPoolable poolable))
                PoolableRef = poolable;
        }

        private void OnEnable() => 
            _limitService.Register(this);

        private void OnDisable() => 
            _limitService.UnRegister(this);

        [Inject]
        public void Construct(ScreenBoundaryLimitService limitService) => 
            _limitService = limitService;
    }
}