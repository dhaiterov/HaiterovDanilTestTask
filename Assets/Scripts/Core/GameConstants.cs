using UnityEngine;

namespace Core {
  public static class GameConstants {
    public static readonly Vector3 SpawnOffsetVector = new(0.5f, 0, 0.5f);
    public const int MouseLeftButtonIndex = 0;
    public const int ParticleLiveTime = 2;
    public const int BulletLayerId = 10;
    public const int EnemyLayerId = 9;
    public const string GameSceneName = "Game";
    public const string SpawnZoneLayer = "TankSpawnZone";
    public const string GroundLayer = "Ground";
    public const string HorizontalInputName = "Horizontal";
    public const string VerticalInputName = "Vertical";
    public const string ShootConfigPath = "Configs/ShootConfig";
    public const string MoveConfigPath = "Configs/MoveConfig";
    public const string GameConfigPath = "Configs/GameConfig";
  }
}