using System;
using System.Collections.Generic;
using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other.Operations.Base;

namespace Code.Services.OutOfBounds.Other
{
    public class RestrictionOperator
    {
        private readonly Dictionary<MotionRestraintOperation, IMotionRestrictionStrategy> _strategies;

        public RestrictionOperator(Dictionary<MotionRestraintOperation, IMotionRestrictionStrategy> strategies) => 
            _strategies = strategies;

        public void ApplyRestriction(RestrictedMovementProvider restricted) => 
            ApplyRestriction(restricted, restricted.Operation);

        public void ApplyRestriction(RestrictedMovementProvider restricted, MotionRestraintOperation operation)
        {
            if (_strategies.TryGetValue(operation, out IMotionRestrictionStrategy strategy))
                strategy.ApplyRestriction(restricted);
            else
                throw new InvalidOperationException($"Unknown operation: {restricted.Operation}");
        }
    }
}