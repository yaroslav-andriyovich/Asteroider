using Code.Services.WorldBounds;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Code.Scene.ObjectEmitting
{
    public class ObjectEmittingZone : MonoBehaviour
    {
        [SerializeField] private float _topScreenOffsetModifier = 1f;
        
        private WoldBoundsService _woldBoundsService;

        [Inject]
        public void Construct(WoldBoundsService woldBoundsService) => 
            _woldBoundsService = woldBoundsService;

        public Vector3 GetRandomPosition()
        {
            Vector3 worldBounds = _woldBoundsService.Bounds;
            Vector3 position = worldBounds;

            position.x = Random.Range(-worldBounds.x, worldBounds.x);
            position.y = worldBounds.y * _topScreenOffsetModifier;
            
            return position;
        }
    }
}