using UnityEngine;

namespace Code.Entities.LazerBullets
{
    public class LazerBullet : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}