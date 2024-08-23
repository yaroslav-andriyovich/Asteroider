using Code.Utils;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class Loot : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        
        private ILootPickupBehaviour _behaviour;

        private void Awake() => 
            _behaviour = GetComponent<ILootPickupBehaviour>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;
            
            _behaviour.PickUp(other.gameObject);
            Destroy(gameObject);
        }
    }
}