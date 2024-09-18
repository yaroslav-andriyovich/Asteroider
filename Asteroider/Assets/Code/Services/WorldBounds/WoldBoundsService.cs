using Code.Services.OutOfBounds.Other;
using UnityEngine;

namespace Code.Services.WorldBounds
{
    public class WoldBoundsService
    {
        public Vector2 Bounds { get; private set; }
        
        public WoldBoundsService()
        {
            Camera camera = Camera.main;
            
            Bounds = ScreenToWorldPoint(camera);
        }
        
        public OutOfBoundsResult CheckOutOfBounds(Vector2 position)
        {
            bool isOutOfHorizontal = position.x < -Bounds.x || position.x > Bounds.x;
            bool isOutOfVertical = position.y < -Bounds.y || position.y > Bounds.y;

            return new OutOfBoundsResult()
            {
                isOutOfHorizontal = isOutOfHorizontal,
                isOutOfVertical = isOutOfVertical
            };
        }

        private Vector2 ScreenToWorldPoint(Camera camera) => 
            camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
    }
}