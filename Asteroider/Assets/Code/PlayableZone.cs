using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlayableZone : MonoBehaviour
    {
        [SerializeField] private string[] _tagsToDestroyInOutRange;
        
        private void Awake() =>
            transform.localScale = CameraUtils.GetStretchedSizeRelative();

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < _tagsToDestroyInOutRange.Length; i++)
            {
                if (other.CompareTag(_tagsToDestroyInOutRange[i]))
                    Destroy(other.gameObject);
            }
        }
    }
}