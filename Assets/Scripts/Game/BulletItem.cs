using System;
using System.Collections;
using Configs;
using Core;
using Data;
using Lean.Pool;
using UnityEngine;
using IPoolable = Lean.Pool.IPoolable;

namespace Game {
  public class BulletItem : MonoBehaviour,IPoolable {
    private bool _isActive;
    private Rigidbody _rigidbody;

    private ShootData _shootData;

    private void Awake() {
      InitComponents();
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent += ApplySpeed;
      _isActive = true;
    }

    private void OnEnable() {
      
      _rigidbody.velocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
      StartCoroutine(TimeToDestroy());
    }

    private void OnDisable() {
      StopAllCoroutines();
    }

    private void OnDestroy() {
      MonoBehaviorLiveManager.OnApplicationFixedUpdateEvent -= ApplySpeed;
    }

    private void OnCollisionEnter(Collision collision) {
      OnCollisionDetect?.Invoke(collision.GetContact(0).point);
      OnCollisionDetect = null;
      _isActive = false;
      LeanPool.Despawn(this);
    
    }

    public event Action<Vector3> OnCollisionDetect;


    public void SetupData(ShootData data) {
      _shootData = data;
    }

    private IEnumerator TimeToDestroy() {
      yield return new WaitForSeconds(2f);
      _isActive = false;
    }

    private void InitComponents() {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void ApplySpeed() {
      if (!_isActive) {
        return;
      }

      _rigidbody.velocity = transform.forward * _shootData.BulletSpeed;
    }

    public void OnSpawn() {
      _isActive = true;
    }

    public void OnDespawn() {
      _isActive = false;
    }
  }
}