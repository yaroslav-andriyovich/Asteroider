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
            if (other.TryGetComponent(out IPoolable poolable))
                poolable.Release();
        }
    }
}