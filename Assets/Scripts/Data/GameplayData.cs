using System;
using Unity.VisualScripting;

namespace Data {
  [Serializable]
  public class GameplayData {
    public int RestartPlayerTime;
    public int RestartBotsTime;
    public int EnemyCount;
  }
}