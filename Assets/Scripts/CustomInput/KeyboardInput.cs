using Core;
using UnityEngine;

namespace CustomInput {
  public class KeyboardInput : IPlayerInput {
    public float GetRotation() {
      return Input.GetAxis(GameConstants.HorizontalInputName);
    }

    public float GetMove() {
      return Input.GetAxis(GameConstants.VerticalInputName);
    }

    public bool IsShootPressed() {
      return Input.GetMouseButtonDown(GameConstants.MouseLeftButtonIndex);
    }
  }
}