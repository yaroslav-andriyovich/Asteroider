using UnityEngine;

namespace Code
{
    public static class Utils
    {
        public static Vector3 GetStretchedSizeRelativeToCamera()
        {
            Camera camera = Camera.main;
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            
            return new Vector3(width, 1, height);
        }
    }
}