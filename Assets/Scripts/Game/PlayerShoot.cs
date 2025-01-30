using System.Collections;
using Configs;
using Core;
using CustomInput;
using Data;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Game {
  public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    private Transform _bulletParent;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private BulletItem BulletPrefab;
    [SerializeField]
    private ParticleItem _particlePrefab;

    private bool _canShot = true;
    private IPlayerInput _input;
    private ShootData _shootData;

    
    [Inject]
    private void Construct(ShootConfig config) {
      _shootData = config.ShootData;
    }
    
    private void Awake() {
      MonoBehaviorLiveManager.OnApplicationUpdateEvent += MakeSoot;
    }

    private void Start() {
      _input = new KeyboardInput();
    }

    private void OnDestroy() {
      MonoBehaviorLiveManager.OnApplicationUpdateEvent -= MakeSoot;
    }

    public void SetupShootable(bool canShot) {
      _canShot = canShot;
    }

    private void MakeSoot() {
      if (_input.IsShootPressed()) {
        if (_canShot) {
          Shoot();
        }
      }
    }

    private IEnumerator CanShoot() {
      yield return new WaitForSeconds(_shootData.ShootDelay);
      _canShot = true;
    }

    private void Shoot() {
      _canShot = false;
      CreateBullet();
      StartCoroutine(CanShoot());
    }

    private void CreateBullet() {
      var item = LeanPool.Spawn(BulletPrefab);
      item.transform.rotation = _bulletSpawnPoint.rotation;
      item.transform.position = _bulletSpawnPoint.position;
      item.SetupData(_shootData);
      item.OnCollisionDetect += OnSoot;
    }
    private void OnSoot(Vector3 pos) {
      var item = LeanPool.Spawn(_particlePrefab);
      item.transform.position = pos;
    }
  }
}