using UnityEngine;

namespace Code
{
    public class GameBackground : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _mesh;
        
        private void Awake() => 
            Resize();

        private void Resize()
        {
            Camera cam = Camera.main;
            float height, width;
            
            float distance = Vector3.Distance(cam.transform.position, transform.position);
            height = 2f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            width = height * cam.aspect;
            
            transform.localScale = new Vector3(width, height, 1);
        }

        /*private void Resize()
        {
            Vector3 scale = new Vector3(1f, 1f, 1f);

            float width = _mesh.bounds.size.x;
            float height = _mesh.bounds.size.y;
     
            float worldScreenHeight = Camera.main.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            scale.x = worldScreenWidth / width;
            scale.y = worldScreenHeight / height;
            
            transform.localScale = scale;
        }*/
    }
}