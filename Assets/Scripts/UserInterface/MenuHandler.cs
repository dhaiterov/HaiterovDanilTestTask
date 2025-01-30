using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {
  [SerializeField]
  private Button _newGameButton;

  private void Awake() {
    _newGameButton.onClick.AddListener(OnStartGameClicked);
  }

  private void OnDestroy() {
    _newGameButton.onClick.RemoveListener(OnStartGameClicked);
  }

  private void OnStartGameClicked() {
    SceneManager.LoadSceneAsync(GameConstants.GameSceneName);
  }
}