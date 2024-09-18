using Code.Entities.Damageables;
using Code.Entities.Death;
using UnityEngine;

namespace Code.Entities.Weapons.Laser
{
    public class LaserWeaponAttack : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField, Min(0f)] private float _attackRange;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _castMask;

        private void Awake() => 
            transform.localRotation = Quaternion.identity;

        private void FixedUpdate()
        {
            if (!isActiveAndEnabled)
                return;

            float hitDistance;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _attackRange, _castMask);
            
            if (hit)
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