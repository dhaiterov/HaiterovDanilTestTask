using System;
using System.Collections;
using UnityEngine;

namespace Core {
  public sealed class CoroutineHandler : MonoBehaviour {
    private static bool _isApplicationIsQuitting;

    private static CoroutineHandler _instance;

    private static CoroutineHandler Instance {
      get {
        if (_isApplicationIsQuitting) {
          return null;
        }

        if (_instance != null) {
          return _instance;
        }

        var gameObject = new GameObject("Coroutine Handler");
        _instance = gameObject.AddComponent<CoroutineHandler>();
        DontDestroyOnLoad(_instance);

        return _instance;
      }
    }

    public static Coroutine StartRoutine(IEnumerator enumerator) {
      return Instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(Coroutine coroutine) {
      if (coroutine == null || Instance == null) {
        return;
      }

      Instance.StopCoroutine(coroutine);
    }

    public static void CallActionWithDelay(float delayInSeconds, Action action) {
      StartRoutine(CallCoroutineWithDelay(delayInSeconds, action));
    }

    public static void StopRoutine(IEnumerator coroutine) {
      if (coroutine == null || Instance == null) {
        return;
      }

      Instance.StopCoroutine(coroutine);
    }

    private static IEnumerator CallCoroutineWithDelay(float delayInSeconds, Action action) {
      yield return new WaitForSeconds(delayInSeconds);
      action();
    }

    [RuntimeInitializeOnLoadMethod]
    private static void SubscribeOnApplicationQuitting() {
      Application.quitting += () => _isApplicationIsQuitting = true;
    }
  }
}