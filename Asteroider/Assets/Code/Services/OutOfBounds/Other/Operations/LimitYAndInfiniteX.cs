using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other.Operations.Base;
using UnityEngine;

namespace Code.Services.OutOfBounds.Other.Operations
{
    public class LimitYAndInfiniteX : IMotionRestrictionStrategy
    {
        private readonly Vector2 _worldBounds;

        public LimitYAndInfiniteX(Vector2 worldBounds) => 
            _worldBounds = worldBounds;

        public void ApplyRestriction(RestrictedMovementProvider restricted)
        {
            Vector2 position = restricted.Rigidbody.position;

            if (position.x >= _worldBounds.x)
                position.x = _worldBounds.x * -1f;
            else if (position.x <= _worldBounds.x * -1f)
                position.x = _worldBounds.x;
            
            position.y = Mathf.Clamp(position.y, _worldBounds.y * -1f, _worldBounds.y);

            restricted.Rigidbody.position = position;
        }
    }
}