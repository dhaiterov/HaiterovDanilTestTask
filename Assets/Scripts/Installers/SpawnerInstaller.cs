using Game;
using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller {
  [SerializeField]
  private SpawnZoneHandler _spawnZoneHandler;

  [SerializeField]
  private PlayerTank _playerTank;

  [SerializeField]
  private BotUnit _botUnit;

  [SerializeField]
  private Transform _gameFieldTransform;

  public override void InstallBindings() {
    Container.BindFactory<PlayerTank, PlayerTank.Factory>()
      .FromComponentInNewPrefab(_playerTank).UnderTransform(_gameFieldTransform);
    Container.BindFactory<BotUnit, BotUnit.Factory>()
      .FromComponentInNewPrefab(_botUnit).UnderTransform(_gameFieldTransform);
    Container.Bind<SpawnZoneHandler>().FromInstance(_spawnZoneHandler).AsSingle();
  }
}