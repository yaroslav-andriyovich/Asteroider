using System;
using UnityEngine;

namespace Code.Entities.Player
{
    [Serializable]
    public class PlayerShipWeapon : IDisposable
    {
        [SerializeField] private Transform _primaryGunPoint;
        [SerializeField] private Transform _secondaryGunPointL;
        [SerializeField] private Transform _secondaryGunPointR;
        
        public void Dispose()
        {
        }
    }
}