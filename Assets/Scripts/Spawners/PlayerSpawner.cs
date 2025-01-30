using Core.Spawners;
using Events;
using Game;
using UnityEngine;
using Zenject;

namespace Spawners {
  public class PlayerSpawner : MonoBehaviour, ISpawner {
    private PlayerTank _playerTank;

    private PlayerTank.Factory _playerTankFactory;
    private SpawnZoneHandler _spawnZoneHandler;

    private void Awake() {
      EventManager.AddListener(EventConstants.SPAWN_PLAYER, Spawn);
      EventManager.AddListener(EventConstants.ON_PLAYER_KILL, Spawn);
      Spawn();
    }

    private void OnDestroy() {
      EventManager.RemoveListener(EventConstants.SPAWN_PLAYER, Spawn);
      EventManager.RemoveListener(EventConstants.ON_PLAYER_KILL, Spawn);
    }

    public void Spawn() {
      if (_playerTank != null) {
        Destroy(_playerTank.gameObject);
      }

      _playerTank = _playerTankFactory.Create();
      _playerTank.transform.position = _spawnZoneHandler.GetRandomPointInSpawnZone(_playerTank.GetCollider());
    }

    [Inject]
    private void Construct(PlayerTank.Factory playerTankFactory, SpawnZoneHandler spawnZoneHandler) {
      _playerTankFactory = playerTankFactory;
      _spawnZoneHandler = spawnZoneHandler;
    }
  }
}