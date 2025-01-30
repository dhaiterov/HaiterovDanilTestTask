using Core;
using UnityEngine;

public class SpawnZoneHandler : MonoBehaviour {
  [SerializeField]
  private BoxCollider _spawnZoneCollider;


  public Vector3 GetRandomPointInSpawnZone(BoxCollider objectCollider) {
    var attempts = 100;

    var objectSize = objectCollider.size + GameConstants.SpawnOffsetVector;

    var layerMask = ~LayerMask.GetMask(GameConstants.SpawnZoneLayer, GameConstants.GroundLayer);

    while (attempts > 0) {
      var spawnPoint = GetRandomPointInBoxCollider(_spawnZoneCollider);

      if (!Physics.CheckBox(spawnPoint, objectSize / 2, Quaternion.identity, layerMask,
            QueryTriggerInteraction.Ignore)) {
        return spawnPoint;
      }

      attempts--;
    }

    return Vector3.zero;
  }

  private Vector3 GetRandomPointInBoxCollider(BoxCollider collider) {
    var center = collider.center;
    var size = collider.size;

    var randomPoint = new Vector3(
      Random.Range(-size.x / 2, size.x / 2),
      Random.Range(-size.y / 2, size.y / 2),
      Random.Range(-size.z / 2, size.z / 2)
    );

    return collider.transform.TransformPoint(center + randomPoint);
  }
}