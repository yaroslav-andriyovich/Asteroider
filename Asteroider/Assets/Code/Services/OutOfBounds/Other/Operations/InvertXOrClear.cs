using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other.Operations.Base;
using Code.Services.WorldBounds;
using UnityEngine;

namespace Code.Services.OutOfBounds.Other.Operations
{
    public class InvertXOrClear : IMotionRestrictionStrategy
    {
        private readonly WoldBoundsService _woldBoundsService;
        private readonly RestrictedCleaner _cleaner;

        public InvertXOrClear(WoldBoundsService woldBoundsService, RestrictedCleaner cleaner)
        {
            _woldBoundsService = woldBoundsService;
            _cleaner = cleaner;
        }

        public void ApplyRestriction(RestrictedMovementProvider restricted)
        {
            if (_woldBoundsService.CheckOutOfBounds(restricted.Rigidbody.position).IsOutOnlyOfHorizontal())
            {
                Vector3 velocity = restricted.Rigidbody.velocity;
            
                velocity.x *= -1f;

                restricted.Rigidbody.velocity = velocity;
            }
            else
                _cleaner.Clear(restricted);
        }
    }
}