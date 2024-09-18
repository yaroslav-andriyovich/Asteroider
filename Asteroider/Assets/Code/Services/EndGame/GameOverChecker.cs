using System;
using Code.Entities.Death;
using Code.Entities.Player;
using Code.Scene.Spawners;
using Code.Services.Player;
using VContainer.Unity;

namespace Code.Services.EndGame
{
    public class GameOverChecker : IInitializable, IDisposable
    {
        private readonly EndGameService _endGameService;
        private readonly PlayerProvider _playerProvider;

        private IDeath _playerDeath;

        public GameOverChecker(EndGameService endGameService, PlayerProvider playerProvider)
        {
            _endGameService = endGameService;
            _playerProvider = playerProvider;
        }

        public void Initialize() =>
            _playerProvider.OnChanged += OnPlayerSpawned;

        public void Dispose()
        {
            _playerProvider.OnChanged -= OnPlayerSpawned;
            _playerDeath.OnHappened -= _endGameService.EndGame;
        }

        private void OnPlayerSpawned()
        {
            _playerDeath = _playerProvider.Player.GetComponent<IDeath>();
            _playerDeath.OnHappened += _endGameService.EndGame;
        }
    }
}