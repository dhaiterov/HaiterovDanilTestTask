using core.eventsystem;
using Events;
using UnityEngine;
using Zenject;

namespace Game {
  public class BotUnit : BaseUnit {
    [SerializeField]
    private BotMovement _movement;

    private bool _isKilled;

    public bool IsKilled() {
      return _isKilled;
    }

    protected override void TakeDamage() {
      base.TakeDamage();
      _destroyView.SetDestroy();
      _movement.StopMoving();
      _isKilled = true;
      EventManager.Invoke(EventConstants.CHECK_ALL_UNITS_KILL);
    }

    public class Factory : PlaceholderFactory<BotUnit> {
    }
  }
}