using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;

namespace Level {
  public class LevelCompleteChecker : MonoBehaviour {
    private readonly List<BotUnit> _botUnits = new();

    private void Awake() {
      EventManager<BotUnit>.AddListener(EventConstants.ON_SPAWN_BOT, OnBotSpawn);
      EventManager.AddListener(EventConstants.CHECK_ALL_UNITS_KILL, CheckAllUnitsKill);
    }

    private void OnDestroy() {
      EventManager<BotUnit>.RemoveListener(EventConstants.ON_SPAWN_BOT, OnBotSpawn);
      EventManager.RemoveListener(EventConstants.CHECK_ALL_UNITS_KILL, CheckAllUnitsKill);
    }

    private void OnBotSpawn(BotUnit bot) {
      _botUnits.Add(bot);
    }

    private void CheckAllUnitsKill() {
      for (var i = 0; i < _botUnits.Count; i++) {
        if (!_botUnits[i].IsKilled()) {
          return;
        }
      }

      EventManager.Invoke(EventConstants.RESTART_LEVEL);
    }
  }
}