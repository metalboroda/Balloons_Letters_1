using System;

namespace Assets.__Game.Resources.Scripts.Settings
{
  [Serializable]
  public class GameSettings
  {
    #region Database
    public string UserName;
    #endregion

    #region LevelManager
    public int OverallLevelIndex;
    public int LevelIndex;
    #endregion

    #region Game Settings
    public bool IsMusicOn = true;
    public bool IsSfxOn = true;
    #endregion

    #region Score
    public int LevelPoints;
    #endregion
  }
}