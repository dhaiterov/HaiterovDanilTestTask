using System.Collections;
using Configs;
using Core;
using Data;
using UnityEngine;
using Zenject;

namespace core.eventsystem {
  public class BotMovement : MonoBehaviour, IMoveable {
    private IEnumerator _hitRotateRoutine;
    private bool _isBlock;
    private EnemyMoveData _moveData;
    private IEnumerator _randomRotateRoutine;
    private Rigidbody _rigidbody;

    [Inject]
    private void Construct(MoveConfig moveConfig) {
      _moveData = moveConfig.BotMoveData;
    }
    
    private void Awake() {
      ApplyParams();
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent += MakeMove;
    }

    private void Start() {
      StartCoroutine(_randomRotateRoutine);
    }

    private void OnDestroy() {
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= MakeMove;
      StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision other) {
      if (other.gameObject.layer != _moveData.GroundLayer) {
        StartCoroutine(_hitRotateRoutine);
      }
    }

    public void Rotation() {
      var targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + _moveData.RotationAngle, 0);
      transform.rotation =
        Quaternion.RotateTowards(transform.rotation, targetRotation, _moveData.RotationSpeed * Time.deltaTime);
    }

    public void Move() {
      if (_isBlock) {
        return;
      }

      var move = 1 * _moveData.MoveSpeed * Time.deltaTime;
      _rigidbody.MovePosition(transform.position + transform.forward * move);
    }
    
    private void ApplyParams() {
      _rigidbody = GetComponent<Rigidbody>();
      _randomRotateRoutine = RotationRoutine();
      _hitRotateRoutine = HitRotationRoutine();
    }

    private void MakeMove() {
      if (_isBlock) {
        return;
      }

      Move();
      StickToGround();
    }


    public void StopMoving() {
      _isBlock = true;
      StopCoroutine(_randomRotateRoutine);
      StopCoroutine(_hitRotateRoutine);
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= MakeMove;
    }

    private IEnumerator HitRotationRoutine() {
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= MakeMove;
      _isBlock = true;
      StopCoroutine(_randomRotateRoutine);
      StartCoroutine(_randomRotateRoutine);
      yield return new WaitForSeconds(_moveData.RotationDelay);
      _isBlock = false;
    }

    private IEnumerator RotationRoutine() {
      while (true) {
        yield return new WaitForSeconds(_moveData.RotationDelay);
        MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= MakeMove;
        StartCoroutine(RotateSmoothly(_moveData.RotationAngle, _moveData.RotationTime));
      }
    }

    private void StickToGround() {
      if (Physics.Raycast(transform.position, Vector3.down, out var hit, _moveData.GroundCheckDistance,
            _moveData.GroundLayer)) {
        var targetPosition = hit.point;
        transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        var groundRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, groundRotation,
          Time.deltaTime * _moveData.RotationMultiplier));
      }
    }

    private IEnumerator RotateSmoothly(float degrees, float duration) {
      var startRotation = transform.rotation;
      var targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + degrees, 0);
      float elapsedTime = 0;

      while (elapsedTime < duration) {
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
      }

      transform.rotation = targetRotation;

      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent += MakeMove;
    }
  }
}