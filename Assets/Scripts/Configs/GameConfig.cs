using Data;
using UnityEngine;

namespace Configs {
  [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
  public class GameConfig : ScriptableObject {
    public GameplayData GameplayData;
  }
}