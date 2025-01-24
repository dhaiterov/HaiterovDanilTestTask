using System;
using System.Collections.Generic;

public static class EventManager {
  private static readonly Dictionary<string, List<Action>> EventDictionary = new();
    
  public static void Invoke(string eventName) {
    if (!EventDictionary.TryGetValue(eventName, out List<Action> eventList)) {
      return;
    }

    for (var index = 0; index < eventList.Count; index++) {
      eventList[index]?.Invoke();
    }
  }

  public static void AddListener(string eventName, Action listener) {
    if (EventDictionary.TryGetValue(eventName, out List<Action> eventList)) {
      eventList.Add(listener);
    } else {
      EventDictionary.Add(eventName, new List<Action> {listener});
    }
  }

  public static void RemoveListener(string eventName, Action listener) {
    if (!EventDictionary.TryGetValue(eventName, out List<Action> eventList)) {
      return;
    }

    eventList.Remove(listener);
    if (eventList.Count == 0) {
      EventDictionary.Remove(eventName);
    }
  }
}

public static class EventManager<T> {
  private static readonly Dictionary<string, List<Action<T>>> EventDictionary = new();

  public static void Invoke(string eventName, T data) {
    if (EventDictionary.TryGetValue(eventName, out List<Action<T>> eventList)) {
      for (var index = 0; index < eventList.Count; index++) {
        eventList[index]?.Invoke(data);
      }
    }

    EventManager.Invoke(eventName);
  }

  public static void AddListener(string eventName, Action<T> listener) {
    if (EventDictionary.TryGetValue(eventName, out List<Action<T>> eventList)) {
      eventList.Add(listener);
    } else {
      EventDictionary.Add(eventName, new List<Action<T>> {listener});
    }
  }

  public static void RemoveListener(string eventName, Action<T> listener) {
    if (!EventDictionary.TryGetValue(eventName, out List<Action<T>> eventList)) {
      return;
    }

    eventList.Remove(listener);
    if (eventList.Count == 0) {
      EventDictionary.Remove(eventName);
    }
  }
}