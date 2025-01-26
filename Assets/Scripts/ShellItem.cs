using core.eventsystem;
using UnityEngine;
using Zenject;

public class ShellItem : MonoBehaviour,ICreatable {
  public void Create(Vector3 startPos) {
  }

  private void Move() {
    
  }
  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag(GameConstants.OBSTACLE_TAG)) {
      //Destroy shell
    } else if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG)) {
      //Destroy shell and enemy
    }
  }
}


public class Factory : PlaceholderFactory<ShellItem> {
}