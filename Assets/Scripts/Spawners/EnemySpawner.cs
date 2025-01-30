using System.Collections.Generic;
using Configs;
using Core;
using Core.Spawners;
using Data;
using Events;
using Game;
using UnityEngine;
using Zenject;

namespace Spawners {
  public class EnemySpawner : MonoBehaviour, ISpawner {
    
    private readonly List<BotUnit> _bots = new();
    private BotUnit.Factory _botTankFactory;
    private SpawnZoneHandler _spawnZoneHandler;
    private GameplayData _data;
    
    [Inject]
    private void Construct(BotUnit.Factory botTankFactory, SpawnZoneHandler spawnZoneHandler,GameConfig config) {
      _botTankFactory = botTankFactory;
      _spawnZoneHandler = spawnZoneHandler;
      _data = config.GameplayData;
    }

    private void Awake() {
      EventManager.AddListener(EventConstants.SPAWN_BOT, Spawn);
      EventManager.AddListener(EventConstants.RESTART_LEVEL, RestartLevel);
    }

    private void Start() {
      Spawn();
    }

    private void OnDestroy() {
      EventManager.RemoveListener(EventConstants.SPAWN_BOT, Spawn);
      EventManager.RemoveListener(EventConstants.RESTART_LEVEL, RestartLevel);
    }

    private void RestartLevel() {
      CoroutineHandler.CallActionWithDelay(_data.RestartBotsTime,RespawnBots);
    }
    
    private void RespawnBots() {
      for (var i = 0; i < _bots.Count; i++) {
        Destroy(_bots[i].gameObject);
      }
      _bots.Clear();
      Spawn();
    }

    public void Spawn() {
      for (var i = 0; i < _data.EnemyCount; i++) {
        var botTank = _botTankFactory.Create();
        _bots.Add(botTank);
        EventManager<BotUnit>.Invoke(EventConstants.ON_SPAWN_BOT, botTank);
        botTank.transform.position = _spawnZoneHandler.GetRandomPointInSpawnZone(botTank.GetCollider());
      }
    }
  }
}