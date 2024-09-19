using System;
using UnityEngine;

namespace Code.Entities.Loots.Base
{
    [Serializable]
    public class RandomLootData
    {
        public LootView prefab;
        [Range(0f, 1f)] public float dropChance;
    }
}