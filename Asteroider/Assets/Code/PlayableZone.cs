using Code.ObjectEmitting;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlayableZone : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IPoolable poolable)
                && (other.CompareTag(GameTags.LazerBullet) 
                    || other.transform.position.z <= transform.position.z - transform.localScale.z / 2f))
            {
                
                poolable.Release();
                return;
            }
            
            if (other.TryGetComponent(out IEmittable emittable))
            {
                Vector3 velocity = emittable.Rigidbody.velocity;

                velocity.x *= -1;
                emittable.Rigidbody.velocity = velocity;
                return;
            }
            
            Destroy(other.gameObject);
        }
    }
}