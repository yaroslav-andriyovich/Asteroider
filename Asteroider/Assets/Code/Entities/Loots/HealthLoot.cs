using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class HealthLoot : MonoBehaviour, ILootPickupBehaviour
    {
        public void PickUp(GameObject user)
        {
            if (user.TryGetComponent(out IHealth health))
                health.Restore();
        }
    }
}