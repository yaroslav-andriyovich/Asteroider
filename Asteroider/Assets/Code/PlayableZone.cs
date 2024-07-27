using UnityEngine;

namespace Code
{
    public class PlayableZone : MonoBehaviour
    {
        private void Awake() => 
            Resize();

        private void Resize()
        {
            Camera camera = Camera.main;
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            
            transform.localScale = new Vector3(width, 1, height);
        }
    }
}