using UnityEngine;

namespace Code.Extensions
{
    public static class CameraExtension
    {
        public static Vector2 GetStretchedSizeRelative(this Camera camera)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            
            return new Vector2(width, height);
        }
    }
}