using UnityEngine;

namespace Code.Entities.Death.AfterDeathBehaviours
{
    public class DestroyAfterDeath : MonoBehaviour, IBehaviourAfterDeath
    {
        private IDeath _death;

        protected virtual void Awake()
        {
            _death = GetComponent<IDeath>();
            
            _death.OnHappened += ExecuteAfterDeath;
        }

        protected void OnDestroy() => 
            _death.OnHappened -= ExecuteAfterDeath;

        public virtual void ExecuteAfterDeath() => 
            Destroy(gameObject);
    }
}