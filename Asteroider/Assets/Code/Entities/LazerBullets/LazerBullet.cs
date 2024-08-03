using Code.Services.Pools;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.LazerBullets
{
    public class LazerBullet : PoolableBase<LazerBullet>
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Asteroid))
                return;
            
            Release();
        }
    }
}