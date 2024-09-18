using Code.Entities.HealthPoints;
using Code.Entities.Loots.Base;
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