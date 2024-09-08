using UnityEngine;

namespace Code
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _speed;

        private void Update()
        {
            float y = Mathf.Repeat(_speed * Time.time, 1f);
            
            _material.mainTextureOffset = new Vector2(0f, y);
        }
    }
}