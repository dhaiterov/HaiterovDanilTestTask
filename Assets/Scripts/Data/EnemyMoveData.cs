using System;
using Configs;

namespace Data {
  [Serializable]
  public class EnemyMoveData : MoveUnitData {
    public float RotationAngle;
    public float RotationDelay;
    public float RotationTime;
  }
}