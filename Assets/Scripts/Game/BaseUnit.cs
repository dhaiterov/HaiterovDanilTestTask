using Core;
using UnityEngine;

namespace Game {
  public class BaseUnit : MonoBehaviour {
    [SerializeField]
    protected DestroyView _destroyView;
    [SerializeField]
    protected BoxCollider _boxCollider;

    protected virtual void OnCollisionEnter(Collision other) {
      if (other.gameObject.layer == GameConstants.BulletLayerId) {
        TakeDamage();
      }
    }

    public BoxCollider GetCollider() {
      return _boxCollider;
    }

    protected virtual void TakeDamage() {
    }
  }
}