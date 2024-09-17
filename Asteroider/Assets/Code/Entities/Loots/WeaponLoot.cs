using Code.Entities.Player.Weapon;
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