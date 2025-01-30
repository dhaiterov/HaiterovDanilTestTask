using UnityEngine;

namespace Game {
  public class DestroyView : MonoBehaviour {
    [SerializeField]
    private ParticleSystem _deathParticles;
    [SerializeField]
    private GameObject _tankTower;


    public void SetDestroy() {
      _tankTower.SetActive(false);
      _deathParticles.gameObject.SetActive(true);
      _deathParticles.Play();
    }
  }
}