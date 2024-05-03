using __Game.Resources.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonHandler : MonoBehaviour, IPointerClickHandler
  {
    private string _balloonValue;
    private bool _correct; 

    public string BalloonValue
    {
      get => _balloonValue;
      private set => _balloonValue = value;
    }

    public bool Correct
    {
      get => _correct;
      private set => _correct = value;
    }

    public void SetBalloonDetails(string value, bool correct, bool tutorial = false)
    {
      _balloonValue = value;
      _correct = correct;

      EventBus<EventStructs.BalloonUiEvent>.Raise(new EventStructs.BalloonUiEvent
      {
        BalloonId = transform.GetInstanceID(),
        BalloonValue = _balloonValue,
        Correct = _correct,
        Tutorial = tutorial
      });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      EventBus<EventStructs.BalloonClickEvent>.Raise(new EventStructs.BalloonClickEvent
      {
        BalloonHandler = this,
        BalloonValue = _balloonValue
      });
    }

    public void DestroyBalloon(bool correct)
    {
      EventBus<EventStructs.BalloonDestroyEvent>.Raise(new EventStructs.BalloonDestroyEvent
      {
        BalloonId = transform.GetInstanceID(),
        Correct = correct
      });

      DOTween.Kill(transform);
      Destroy(gameObject);
    }
  }
}
