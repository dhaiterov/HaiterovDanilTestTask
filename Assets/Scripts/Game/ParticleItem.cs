using Core;
using Lean.Pool;
using UnityEngine;

namespace Game {
  public class ParticleItem : MonoBehaviour, IPoolable {
    [SerializeField]
    private ParticleSystem _particle;

    public void OnSpawn() {
      _particle.Play();
      CoroutineHandler.CallActionWithDelay(GameConstants.ParticleLiveTime, () => LeanPool.Despawn(this));
    }

    public void OnDespawn() {
      gameObject.SetActive(false);
    }
  }
}