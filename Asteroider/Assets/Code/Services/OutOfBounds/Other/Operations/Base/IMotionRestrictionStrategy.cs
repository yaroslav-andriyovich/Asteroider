using Code.Services.OutOfBounds.Components;

namespace Code.Services.OutOfBounds.Other.Operations.Base
{
    public interface IMotionRestrictionStrategy
    {
        void ApplyRestriction(RestrictedMovementProvider restricted);
    }
}