using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Settings
{
  public class SettingsManager
  {
    private const string SettingsFileName = "settings.json";

    public static T LoadSettings<T>()
    {
      T settings = default;
      string filePath = Application.persistentDataPath + "/" + SettingsFileName;

      if (System.IO.File.Exists(filePath))
      {
        string json = System.IO.File.ReadAllText(filePath);

        settings = JsonUtility.FromJson<T>(json);
      }

      return settings;
    }

    public static void SaveSettings<T>(T settings)
    {
      string json = JsonUtility.ToJson(settings);
      string filePath = Application.persistentDataPath + "/" + SettingsFileName;

      System.IO.File.WriteAllText(filePath, json);
    }

    public static void ResetSettings()
    {
      string filePath = Application.persistentDataPath + "/" + SettingsFileName;

      if (System.IO.File.Exists(filePath))
      {
        System.IO.File.Delete(filePath);
      }
    }
  }
}