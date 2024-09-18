using System;

namespace Code.Entities.Shields
{
    public interface IShield
    {
        float Max { get; }
        float Current { get; }
        event Action OnChanged;
        
        bool TryAbsorbDamage(float damage, out float remainingDamage);
        void Restore();
    }
}