using __Game.Resources.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonMovement : MonoBehaviour
  {
    private float _movementSpeed;
    private Vector3 _initPosition;
    private Vector3 _movementTarget;

    private BalloonController _balloonController;

    private void Awake()
    {
      _balloonController = GetComponent<BalloonController>();

      _initPosition = transform.position;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
      _movementSpeed = movementSpeed;
    }

    public void SetMovementTarget(Vector3 xTarget, float yTarget, float yOffset)
    {
      _movementTarget = new Vector3(xTarget.x, yTarget +  yOffset, 0);
    }

    public void MoveToTarget()
    {
      transform.DOMove(_movementTarget, _movementSpeed).SetSpeedBased(true).OnComplete(() =>
      {
        EventBus<EventStructs.BalloonReMovementEvent>.Raise(new EventStructs.BalloonReMovementEvent
        {
          BalloonController = _balloonController
        });

        transform.DOMove(_initPosition, 0);
      });
    }
  }
}