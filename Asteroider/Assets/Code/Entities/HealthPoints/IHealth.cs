using System;

namespace Code.Entities.HealthPoints
{
    public interface IHealth
    {
        event Action OnChanged;
        float Max { get; }
        float Current { get; }

        void TakeDamage(float damage);
        void Restore();
    }
}