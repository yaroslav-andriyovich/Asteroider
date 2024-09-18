using System;

namespace Code.Entities.Death
{
    public interface IDeath
    {
        event Action OnHappened;

        void Die();
    }
}