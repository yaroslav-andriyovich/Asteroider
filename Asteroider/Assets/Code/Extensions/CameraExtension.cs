using UnityEngine;

namespace Code.Extensions
{
    public static class CameraExtension
    {
        public static Vector3 GetStretchedSizeRelative(this Camera camera)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            
            return new Vector3(width, 1, height);
        }
    }
}