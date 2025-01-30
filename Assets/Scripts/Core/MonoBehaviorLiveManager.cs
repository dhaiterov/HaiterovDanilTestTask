using System;
using UnityEngine;

namespace Core {
  public class MonoBehaviorLiveManager : MonoBehaviour {
    private void Awake() {
      DontDestroyOnLoad(gameObject);
    }

    private void Update() {
      OnApplicationUpdateEvent?.Invoke();
    }

    private void FixedUpdate() {
      OnApplicationFixedUpdateEvent?.Invoke();
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (!hasFocus) {
        OnApplicationUnfocusedEvent?.Invoke();
      } else {
        OnApplicationFocusedEvent?.Invoke();
      }
    }

    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus) {
        OnApplicationPausedEvent?.Invoke();
      } else {
        OnApplicationUnpausedEvent?.Invoke();
      }
    }

    private void OnApplicationQuit() {
      OnApplicationQuitEvent?.Invoke();
    }

    public static event Action OnApplicationUpdateEvent;
    public static event Action OnApplicationFixedUpdateEvent;
    public static event Action OnApplicationPausedEvent;
    public static event Action OnApplicationUnpausedEvent;
    public static event Action OnApplicationFocusedEvent;
    public static event Action OnApplicationUnfocusedEvent;
    public static event Action OnApplicationQuitEvent;
  }
}