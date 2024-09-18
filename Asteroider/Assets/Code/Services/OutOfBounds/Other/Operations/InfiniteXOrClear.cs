using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other.Operations.Base;
using Code.Services.WorldBounds;
using UnityEngine;

namespace Code.Services.OutOfBounds.Other.Operations
{
    public class InfiniteXOrClear : IMotionRestrictionStrategy
    {
        private readonly WoldBoundsService _woldBoundsService;
        private readonly RestrictedCleaner _cleaner;

        public InfiniteXOrClear(WoldBoundsService woldBoundsService, RestrictedCleaner cleaner)
        {
            _woldBoundsService = woldBoundsService;
            _cleaner = cleaner;
        }

        public void ApplyRestriction(RestrictedMovementProvider restricted)
        {
            if (_woldBoundsService.CheckOutOfBounds(restricted.Rigidbody.position).IsOutOnlyOfHorizontal())
            {
                Vector2 bounds = _woldBoundsService.Bounds;
                Vector2 position = restricted.Rigidbody.position;

                if (position.x >= bounds.x)
                    position.x = bounds.x * -1f;
                else if (position.x <= bounds.x * -1f)
                    position.x = bounds.x;
                
                restricted.Rigidbody.position = position;
            }
            else
                _cleaner.Clear(restricted);
        }
    }
}