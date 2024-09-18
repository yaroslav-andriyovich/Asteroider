using System;
using Code.Entities.Weapons.Base;
using UnityEngine;

namespace Code.Entities.Weapons.Laser
{
    public class LaserWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private AudioSource _audio;

        public event Action OnFire;
        public bool IsActive => gameObject.activeSelf;

        private void Awake() => 
            Deactivate();

        public void Activate()
        {
            gameObject.SetActive(true);
            _audio.Stop();
            _audio.Play();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void SetGunPoints(Transform[] points)
        {
        }
    }
}