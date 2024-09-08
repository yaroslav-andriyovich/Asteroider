using Code.Services.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.ObjectEmitting
{
    public class ObjectEmittingZone : MonoBehaviour
    {
        private float _dividedLocalScaleX; 
        private PoolService _poolService;

        private void Awake() => 
            _dividedLocalScaleX = transform.localScale.x / 2;

        public Vector3 GetRandomPosition()
        {
            Vector3 position = transform.position;

            position.x += Random.Range(-_dividedLocalScaleX, _dividedLocalScaleX);
            return position;
        }
    }
}