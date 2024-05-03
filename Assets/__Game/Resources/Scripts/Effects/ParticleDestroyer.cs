using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Effects
{
  public class ParticleDestroyer : MonoBehaviour
  {
    [SerializeField] private float _destroyTime = 1.5f;

    private void Awake()
    {
      Destroy(gameObject, _destroyTime);
    }
  }
}