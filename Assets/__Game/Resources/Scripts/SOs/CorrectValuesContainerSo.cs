using UnityEngine;

namespace Assets.__Game.Resources.Scripts.SOs
{
  [CreateAssetMenu(fileName = "CorrectValuesContainer", menuName = "SOs/Containers/CorrectValuesContainer")]
  public class CorrectValuesContainerSo : ScriptableObject
  {
    [SerializeField] private string[] _correctValues;

    public string[] CorrectValues
    {
      get => _correctValues;
      private set => _correctValues = value;
    }
  }
}