using Data;
using UnityEngine;

namespace Configs {
  [CreateAssetMenu(fileName = "ShootConfig", menuName = "Game/ShootConfig")]
  public class ShootConfig : ScriptableObject {
    public ShootData ShootData;
  }
}