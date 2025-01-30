using Configs;
using Core;
using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller {
  public override void InstallBindings() {
    var shootConfig = Resources.Load<ShootConfig>(GameConstants.ShootConfigPath);
    var moveConfig = Resources.Load<MoveConfig>(GameConstants.MoveConfigPath);
    var gameConfig = Resources.Load<GameConfig>(GameConstants.GameConfigPath);
    Container.BindInstance(shootConfig).AsSingle();
    Container.BindInstance(moveConfig).AsSingle();
    Container.BindInstance(gameConfig).AsSingle();
  }
}