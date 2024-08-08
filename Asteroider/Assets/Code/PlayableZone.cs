using Code.ObjectEmitting;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlayableZone : MonoBehaviour
    {
        private void Awake()
        {
            Vector3 stretchedSizeRelative = CameraUtils.GetStretchedSizeRelative();

            stretchedSizeRelative.y = transform.localScale.y;
            
            transform.localScale = stretchedSizeRelative;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTags.Player))
            {
                Vector3 position = other.transform.position;
                
                if (position.z <= transform.position.z - transform.localScale.z / 2f)
                {
                    position.z = transform.position.z - transform.localScale.z / 2f;
                    other.transform.position = position;
                }
                else if (position.z >= transform.position.z + transform.localScale.z / 2f)
                {
                    position.z = transform.position.z + transform.localScale.z / 2f;
                    other.transform.position = position;
                }
                else if (position.x <= transform.position.x - transform.localScale.x / 2f)
                {
                    position.x = transform.position.x - transform.localScale.x / 2f;
                    other.transform.position = position;
                }
                else if (position.x >= transform.position.x + transform.localScale.x / 2f)
                {
                    position.x = transform.position.x + transform.localScale.x / 2f;
                    other.transform.position = position;
                }
            }
            
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
            }
        }
    }
}