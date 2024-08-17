using System;

namespace Code.Entities.Components
{
    public interface IHealth : IDamagable
    {
        event Action OnChanged;
        float Max { get; }
        float Current { get; }
    }
}