using UnityEngine;
using VContainer.Unity;

namespace Code.Entities.Player
{
    public class PlayerMotionLimiter : IFixedTickable
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _playableZone;

        public PlayerMotionLimiter(Rigidbody rigidbody, PlayableZone playableZone)
        {
            _rigidbody = rigidbody;
            _playableZone = playableZone.transform;
        }

        public void FixedTick()
        {
            Vector3 position = _rigidbody.position;
            Vector3 playableZonePosition = _playableZone.position;
            Vector3 playableZoneHalfSize = _playableZone.localScale / 2f;

            position.x = Mathf.Clamp(position.x, playableZonePosition.x - playableZoneHalfSize.x, playableZonePosition.x + playableZoneHalfSize.x);
            position.z = Mathf.Clamp(position.z, playableZonePosition.z - playableZoneHalfSize.z, playableZonePosition.z + playableZoneHalfSize.z);

            _rigidbody.position = position;
        }
    }
}