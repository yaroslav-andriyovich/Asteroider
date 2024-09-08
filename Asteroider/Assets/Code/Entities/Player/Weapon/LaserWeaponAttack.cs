using Code.Entities.Components;
using Code.Entities.Components.Death;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Player.Weapon
{
    public class LaserWeaponAttack : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField, Min(0f)] private float _attackRange;
        [SerializeField, Min(0f)] private float _attackWidth;
        [SerializeField] private float _damage;

        private void Update()
        {
            transform.rotation = Quaternion.identity;
            
            if (!isActiveAndEnabled)
                return;

            float hitDistance;
            
            if (Physics.Raycast(GetRay(), out RaycastHit hit, _attackRange) 
            //if (Physics.BoxCast(transform.position, new Vector3(_attackWidth, 1f, _attackRange) / 2f, transform.forward, out RaycastHit hit, transform.rotation, _attackRange) 
                && !hit.transform.CompareTag(GameTags.PlayableZone))
            {
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
                else if (hit.transform.TryGetComponent(out IDeath death))
                    death.Die();

                hitDistance = hit.distance;
            }
            else
                hitDistance = _attackRange;

            SetLineSecondPoint(hitDistance);
        }

        private Ray GetRay()
        {
            Ray ray = new Ray(transform.position, transform.forward * _attackRange);
            return ray;
        }

        private void SetLineSecondPoint(float hitDistance) => 
            _line.SetPosition(1, Vector3.forward * hitDistance);
    }
}