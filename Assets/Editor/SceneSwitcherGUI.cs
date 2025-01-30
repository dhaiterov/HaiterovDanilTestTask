using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
  [InitializeOnLoad]
  public class SceneSwitcherGUI {
    private static readonly string WindowTitle = "Switcher";
    private static readonly string SceneMenuPath = "Assets/Scenes/MainMenu.unity";
    private static readonly string SceneGamePath = "Assets/Scenes/Game.unity";
    private static readonly string ButtonNameSceneMenu = "Menu";
    private static readonly string ButtonNameSceneGame = "Game";
    private static readonly string WindowPosX = "WindowPosX";
    private static readonly string WindowPosY = "WindowPosY";
    private static readonly string WindowWidth = "WindowWidth";
    private static readonly string WindowHeight = "WindowHeight";
    private static readonly string WindowCollapsed = "WindowCollapsed";
    private static readonly string WindowVisible = "WindowVisible";

    private static readonly int WindowID = 123456;
    private static readonly Vector2 ControlButtonSize = new Vector2(20, 20);
    private static readonly Vector2 MainButtonSize = new Vector2(60, 20);

    private static Rect _windowRect = new Rect(100, 100, 60, 50);
    private static bool _windowVisible = true;
    private static bool _isCollapsed;

    [MenuItem("Tools/Scene Switcher")]
    public static void ToggleWindow() {
      _windowVisible = !_windowVisible;
    }

    private static void OnSceneGUI (SceneView sceneView) {
      if (!_windowVisible) {
        return;
      }

      Handles.BeginGUI();

      _windowRect = GUILayout.Window(WindowID, _windowRect, DrawSceneWindow, WindowTitle);

      Handles.EndGUI();
    }

    private static void DrawSceneWindow (int id) {
      if (Event.current.type == EventType.Layout) {
        SaveWindowPosition();
      }

      GUILayout.BeginHorizontal();

      GUILayout.EndHorizontal();

      if (!_isCollapsed) {
        DrawButton(ButtonNameSceneMenu, SceneMenuPath);
        DrawButton(ButtonNameSceneGame, SceneGamePath);
      }

      GUI.DragWindow();
    }

    private static void DrawButton (string buttonName, string pathName) {
      if (GUILayout.Button(buttonName, GUILayout.Width(MainButtonSize.x), GUILayout.Height(MainButtonSize.y))) {
        EditorSceneManager.OpenScene(pathName);
      }
    }

    private static void SaveWindowPosition() {
      EditorPrefs.SetFloat(WindowPosX, _windowRect.x);
      EditorPrefs.SetFloat(WindowPosY, _windowRect.y);
      EditorPrefs.SetFloat(WindowWidth, _windowRect.width);
      EditorPrefs.SetFloat(WindowHeight, _windowRect.height);
      EditorPrefs.SetBool(WindowCollapsed, _isCollapsed);
      EditorPrefs.SetBool(WindowVisible, _windowVisible);
    }

    private static void LoadWindowPosition() {
      if (EditorPrefs.HasKey(WindowPosX) && EditorPrefs.HasKey(WindowPosY) && EditorPrefs.HasKey(WindowWidth) && EditorPrefs.HasKey(WindowHeight)
          && EditorPrefs.HasKey(WindowCollapsed) && EditorPrefs.HasKey(WindowVisible)) {
        _windowRect.x = EditorPrefs.GetFloat(WindowPosX);
        _windowRect.y = EditorPrefs.GetFloat(WindowPosY);
        _windowRect.width = EditorPrefs.GetFloat(WindowWidth);
        _windowRect.height = EditorPrefs.GetFloat(WindowHeight);
        _isCollapsed = EditorPrefs.GetBool(WindowCollapsed);
        _windowVisible = EditorPrefs.GetBool(WindowVisible);
      }
    }

    static SceneSwitcherGUI() {
      SceneView.duringSceneGui += OnSceneGUI; ;
      LoadWindowPosition();
    }

    private void OnDestroy() {
      SaveWindowPosition();
    }
  }
}