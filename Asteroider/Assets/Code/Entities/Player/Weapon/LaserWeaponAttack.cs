using Code.Effects;
using Code.Entities.Components;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player.Weapon
{
    public class LaserWeaponAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private MonoPool<ExplosionEffect> _explosionEffectsPool;
        
        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }

        [Inject]
        public void Construct(PoolService poolService) =>
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone))
                return;

            if (!other.CompareTag(GameTags.Asteroid))
            {
                if (other.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
                
                ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

                effect.Play();
            }
        }
    }
}