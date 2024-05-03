using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Tools
{
  public class RandomScreenPositionGenerator
  {
    private readonly Camera _mainCamera;

    public RandomScreenPositionGenerator(Camera camera)
    {
      _mainCamera = camera;
    }

    public Vector3 GetRandomXPosition()
    {
      float screenWidth = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
      float randomX = Random.Range(-screenWidth, screenWidth);

      return new Vector3(randomX, 0, 0);
    }

    public float GetBottomYPosition()
    {
      float bottomY = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

      return bottomY;
    }

    public float GetTopYPosition()
    {
      float topY = _mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

      return topY;
    }
  }
}