using Code.Entities.Loots.Base;
using Code.Entities.Shields;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class ShieldLoot : Loot
    {
        public override void Collect(GameObject player)
        {
            if (player.TryGetComponent(out IShield shield))
                shield.Restore();
        }
    }
}