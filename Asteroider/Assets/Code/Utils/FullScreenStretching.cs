using Code.Extensions;
using UnityEngine;

namespace Code.Utils
{
    public class FullScreenStretching : MonoBehaviour
    {
        [SerializeField] private bool _saveXScale;
        [SerializeField] private bool _saveYScale;
        [Space]
        [SerializeField] private Vector3 _modifier = Vector3.one;
        [Space]
        [SerializeField] private bool _swapScaleYZ;

        private void Awake()
        {
            Vector2 stretchedSizeRelative = Camera.main.GetStretchedSizeRelative();
            Vector3 scale = Vector3.one;

            if (_saveXScale)
                scale.x = transform.localScale.x;
            else
                scale.x = stretchedSizeRelative.x * _modifier.x;

            if (_saveYScale)
                scale.y = transform.localScale.y;
            else
            {
                if (_swapScaleYZ)
                    scale.y = stretchedSizeRelative.y * _modifier.z;
                else
                    scale.y = stretchedSizeRelative.y * _modifier.y;
            }

            if (_swapScaleYZ)
                scale.z = stretchedSizeRelative.y * _modifier.y;
            else
                scale.z = transform.localScale.z;

            transform.localScale = scale;
        }
    }
}