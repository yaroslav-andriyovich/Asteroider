using Code.Entities.Loots.Base;
using Code.Services.Player;

namespace Code.Services.Loots
{
    public class LootCollectionService
    {
        private readonly PlayerProvider _playerProvider;

        public LootCollectionService(PlayerProvider playerProvider) => 
            _playerProvider = playerProvider;

        public void OnCollect(Loot loot)
        {
            loot.Collect(_playerProvider.Player);
        }
    }
}