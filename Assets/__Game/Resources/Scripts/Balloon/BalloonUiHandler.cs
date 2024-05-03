using __Game.Resources.Scripts.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonUiHandler : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _numberText;
    [Header("Tutorial Param's")]
    [SerializeField] private Image _glowingImage;
    [SerializeField] private Color _correctGlowingColor;
    [SerializeField] private Color _incorrectGlowingColor;

    private EventBinding<EventStructs.BalloonUiEvent> _balloonUiEvent;

    private void OnEnable()
    {
      _balloonUiEvent = new EventBinding<EventStructs.BalloonUiEvent>(ReceiveBumber);
      _balloonUiEvent = new EventBinding<EventStructs.BalloonUiEvent>(SetTutorialGlowingColor);
    }

    private void OnDisable()
    {
      _balloonUiEvent.Remove(ReceiveBumber);
      _balloonUiEvent.Remove(SetTutorialGlowingColor);
    }

    private void ReceiveBumber(EventStructs.BalloonUiEvent balloonUiEvent)
    {
      if (this == null) return;
      if (balloonUiEvent.BalloonId != transform.GetInstanceID()) return;

      _numberText.SetText(balloonUiEvent.BalloonValue.ToString());
    }

    private void SetTutorialGlowingColor(EventStructs.BalloonUiEvent balloonUiEvent)
    {
      if (balloonUiEvent.BalloonId != transform.GetInstanceID()) return;
      if (balloonUiEvent.Tutorial == true)
        _glowingImage.gameObject.SetActive(true);
      else
        return;

      _glowingImage.color =balloonUiEvent.Correct == true ? _correctGlowingColor : _incorrectGlowingColor;
    }
  }
}