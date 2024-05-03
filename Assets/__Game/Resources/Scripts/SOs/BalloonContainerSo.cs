using Assets.__Game.Resources.Scripts.Balloon;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.SOs
{
  [CreateAssetMenu(fileName = "BalloonContainer", menuName = "SOs/Containers/BalloonContainer")]
  public class BalloonContainerSo : ScriptableObject
  {
    [SerializeField] private BalloonHandler[] _balloons;

    private int _lastIndex = -1;

    public BalloonHandler[] Balloons
    {
      get => _balloons;
      private set => _balloons = value;
    }

    public BalloonHandler GetRandomBalloon()
    {
      if (_balloons == null || _balloons.Length == 0) return null;
      if (_balloons.Length == 1) return _balloons[0];

      int randomIndex = _lastIndex;

      while (randomIndex == _lastIndex)
      {
        randomIndex = Random.Range(0, _balloons.Length);
      }

      _lastIndex = randomIndex;

      return _balloons[randomIndex];
    }
  }
}