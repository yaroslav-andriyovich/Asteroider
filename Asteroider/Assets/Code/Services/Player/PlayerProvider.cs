using System;
using UnityEngine;

namespace Code.Services.Player
{
    public class PlayerProvider
    {
        public event Action OnChanged;
        public GameObject Player
        {
            get => _player;
            set
            {
                _player = value;
                OnChanged?.Invoke();
            }
        }

        private GameObject _player;
    }
}