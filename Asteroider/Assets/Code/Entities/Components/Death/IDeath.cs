using System;

namespace Code.Entities.Components.Death
{
    public interface IDeath
    {
        event Action OnHappened;
    }
}