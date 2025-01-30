using Configs;
using Core;
using core.eventsystem;
using Data;
using Events;
using UnityEngine;
using Zenject;

namespace Game {
  public class PlayerTank : BaseUnit {
  
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerShoot _playerShoot;

    private bool _canTouch = true;
    private GameplayData _data;
    
    [Inject]
    private void Construct(GameConfig config) {
      _data = config.GameplayData;
    }
    protected override void OnCollisionEnter(Collision other) {
      if (!_canTouch) {
        return;
      }

      if (other.gameObject.layer == GameConstants.EnemyLayerId) {
        _canTouch = false;
        _destroyView.SetDestroy();
        _playerMovement.SetupBlock(true);
        _playerShoot.SetupShootable(false);
        CoroutineHandler.CallActionWithDelay(_data.RestartPlayerTime, () => EventManager.Invoke(EventConstants.ON_PLAYER_KILL));
      }
    }


    public class Factory : PlaceholderFactory<PlayerTank> {
    }
  }
}