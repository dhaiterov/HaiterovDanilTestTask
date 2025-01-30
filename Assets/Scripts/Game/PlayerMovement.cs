using Configs;
using Core;
using CustomInput;
using Data;
using Game;
using UnityEngine;
using Zenject;

namespace core.eventsystem {
  public class PlayerMovement : MonoBehaviour, IMoveable {
    public LayerMask groundLayer;
    private IPlayerInput _input;
    private bool _isBlock;
    private PlayerMoveData _moveData;
    private Rigidbody _rigidbody;

    
    [Inject]
    private void Construct(MoveConfig moveConfig) {
      _moveData = moveConfig.PlayerMoveData;
    }
    
    private void Awake() {
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent += CheckControl;
    }

    private void Start() {
      _rigidbody = GetComponent<Rigidbody>();
      _input = new KeyboardInput();
    }

    private void OnDestroy() {
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= CheckControl;
    }

    public void Rotation() {
      var rotation = _input.GetRotation() * _moveData.RotationSpeed * Time.deltaTime;
      _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, rotation, 0));
    }

    public void Move() {
      var move = _input.GetMove() * _moveData.MoveSpeed * Time.deltaTime;
      _rigidbody.MovePosition(transform.position + transform.forward * move);
    }

    public void SetupBlock(bool isBlock) {
      _isBlock = isBlock;
    }

    private void CheckControl() {
      if (_isBlock) {
        MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= CheckControl;
        return;
      }

      Rotation();
      Move();
      StickToGround();
    }

    private void StickToGround() {
      if (Physics.Raycast(transform.position, Vector3.down, out var hit, _moveData.GroundCheckDistance, groundLayer)) {
        var targetPosition = hit.point;
        transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        var groundRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, groundRotation,
          Time.deltaTime * _moveData.RotationMultiplier));
      }
    }
  }
}