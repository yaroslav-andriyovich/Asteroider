using UnityEngine;

namespace Code.Utils
{
    public static class CameraUtils
    {
        public static Vector3 GetStretchedSizeRelative()
        {
            Camera camera = Camera.main;
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            
            return new Vector3(width, 1, height);
        }
    }
}