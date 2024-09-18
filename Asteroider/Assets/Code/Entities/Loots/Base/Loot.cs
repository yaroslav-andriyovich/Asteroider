using UnityEngine;

namespace Code.Entities.Loots.Base
{
    public abstract class Loot : MonoBehaviour
    {
        public abstract void Collect(GameObject player);
    }
}