using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class ShieldLoot : MonoBehaviour, ILootPickupBehaviour
    {
        public void PickUp(GameObject user)
        {
            if (user.TryGetComponent(out IShield shield))
                shield.Restore();
        }
    }
}