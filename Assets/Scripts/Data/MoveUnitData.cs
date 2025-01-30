using System;
using UnityEngine;

namespace Data {
  [Serializable]
  public class MoveUnitData {
    public float MoveSpeed;
    public float RotationSpeed;
    public float RotationMultiplier;
    public LayerMask GroundLayer;
    public float GroundCheckDistance;
  }
}