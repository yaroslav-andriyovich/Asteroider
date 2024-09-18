using Code.Entities.Damageables;
using Code.Entities.Death;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Weapons.Laser
{
    public class LaserWeaponAttack : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField, Min(0f)] private float _attackRange;
        [SerializeField, Min(0f)] private float _attackWidth;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _castMask;

        private void Update()
        {
            transform.rotation = Quaternion.identity;
            
            if (!isActiveAndEnabled)
                return;

            float hitDistance;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _attackRange, _castMask);
            
            if (hit && !hit.transform.CompareTag(GameTags.PlayableZone))
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

        private void SetLineSecondPoint(float hitDistance) => 
            _line.SetPosition(1, Vector3.up * hitDistance);
    }
}