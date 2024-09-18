using Code.Entities.Loots.Base;
using Code.Entities.Player;
using Code.Entities.Weapons.Base;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class WeaponLoot : MonoBehaviour, ILootPickupBehaviour
    {
        [SerializeField] private GameObject _weapon;
        
        public void PickUp(GameObject user)
        {
            PlayerWeapon playerWeapon = user.GetComponent<PlayerWeapon>();
            IWeapon newWeapon = Instantiate(_weapon).GetComponent<IWeapon>();

            playerWeapon.ChangeWeapon(newWeapon);
        }
    }
}