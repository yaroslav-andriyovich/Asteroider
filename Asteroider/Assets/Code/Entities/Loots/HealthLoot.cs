using Code.Entities.HealthPoints;
using Code.Entities.Loots.Base;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class HealthLoot : Loot
    {
        public override void Collect(GameObject player)
        {
            if (player.TryGetComponent(out IHealth health))
                health.Restore();
        }
    }
}