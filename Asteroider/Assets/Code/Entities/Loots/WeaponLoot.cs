using Code.Entities.Loots.Base;
using Code.Entities.Player;
using Code.Entities.Weapons.Base;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class WeaponLoot : Loot
    {
        [SerializeField] private GameObject _weapon;

        public override void Collect(GameObject player)
        {
            PlayerWeapon playerWeapon = player.GetComponent<PlayerWeapon>();
            IWeapon newWeapon = Instantiate(_weapon).GetComponent<IWeapon>();

            playerWeapon.ChangeWeapon(newWeapon);
        }
    }
}