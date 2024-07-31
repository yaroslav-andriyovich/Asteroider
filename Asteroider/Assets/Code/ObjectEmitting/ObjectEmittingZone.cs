using Code.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.ObjectEmitting
{
    public class ObjectEmittingZone : MonoBehaviour
    {
        private float _dividedLocalScaleX;

        private void Awake()
        {
            Resize();
            _dividedLocalScaleX = transform.localScale.x / 2;
        }

        private void Resize()
        {
            Vector3 scale = transform.localScale;
            
            scale.x = CameraUtils.GetStretchedSizeRelative().x;
            transform.localScale = scale;
        }
        public Vector3 GetRandomPosition()
        {
            Vector3 position = transform.position;

            position.x += Random.Range(-_dividedLocalScaleX, _dividedLocalScaleX);
            return position;
        }
    }
}