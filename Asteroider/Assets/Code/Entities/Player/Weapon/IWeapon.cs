using System;
using UnityEngine;

namespace Code.Entities.Player.Weapon
{
    public interface IWeapon
    {
        event Action OnFire;
        bool IsActive { get; }
        GameObject gameObject { get; }
        
        void Activate();
        void Deactivate();
        void SetGunPoints(Transform[] points);
    }
}