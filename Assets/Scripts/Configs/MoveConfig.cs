using Data;
using UnityEngine;

namespace Configs {
  [CreateAssetMenu(fileName = "MoveConfig", menuName = "Game/MoveConfig")]
  public class MoveConfig : ScriptableObject {
    public PlayerMoveData PlayerMoveData;
    public EnemyMoveData BotMoveData;
  }
}