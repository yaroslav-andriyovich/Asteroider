using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other.Operations.Base;

namespace Code.Services.OutOfBounds.Other.Operations
{
    public class ClearObjectOperation : IMotionRestrictionStrategy
    {
        private readonly RestrictedCleaner _cleaner;

        public ClearObjectOperation(RestrictedCleaner cleaner) => 
            _cleaner = cleaner;

        public void ApplyRestriction(RestrictedMovementProvider restricted) => 
            _cleaner.Clear(restricted);
    }
}