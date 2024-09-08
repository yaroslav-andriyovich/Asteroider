using System;
using Code.Entities.Components.Death;
using Code.Entities.Player;
using VContainer.Unity;

namespace Code.Services
{
    public class GameOverChecker : IInitializable, IDisposable
    {
        private readonly EndGameService _endGameService;
        private readonly PlayerSpawner _playerSpawner;

        private IDeath _playerDeath;

        public GameOverChecker(EndGameService endGameService, PlayerSpawner playerSpawner)
        {
            _endGameService = endGameService;
            _playerSpawner = playerSpawner;
        }

        public void Initialize() => 
            _playerSpawner.OnSpawned += OnPlayerSpawned;

        public void Dispose()
        {
            _playerSpawner.OnSpawned -= OnPlayerSpawned;
            _playerDeath.OnHappened -= _endGameService.EndGame;
        }

        private void OnPlayerSpawned(PlayerShip ship)
        {
            _playerDeath = ship.GetComponent<IDeath>();
            
            _playerDeath.OnHappened += _endGameService.EndGame;
        }
    }
}