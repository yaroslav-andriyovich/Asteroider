using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlayableZone : MonoBehaviour
    {
        private void Awake() =>
            transform.localScale = CameraUtils.GetStretchedSizeRelative();

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTags.Asteroid))
                Destroy(other.gameObject);
        }
    }
}