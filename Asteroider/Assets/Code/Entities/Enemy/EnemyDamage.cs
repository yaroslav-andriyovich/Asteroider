using Code.Entities.Components;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyDamage : MonoBehaviour, IDamagable
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private EnemyDeath _death;

        private IHealth _health;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            
            _trigger.OnTrigger += OnTrigger;
        }

        private void OnDestroy() => 
            _trigger.OnTrigger -= OnTrigger;

        public void TakeDamage(float damage) => 
            _health.TakeDamage(damage);

        private void OnTrigger(Collider other)
        {
            if (other.CompareTag(GameTags.Player) 
                || other.CompareTag(GameTags.Obstacle)
                || other.CompareTag(GameTags.Enemy))
                _death.Die();
        }
    }
}