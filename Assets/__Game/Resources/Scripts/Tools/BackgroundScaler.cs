using UnityEngine;

namespace Assets.__Game.Scripts.Tools
{
  public class BackgroundScaler : MonoBehaviour
  {
    [SerializeField] private float _zPosition = 5f;
    [SerializeField] private Vector2 _scaleOffset = Vector2.zero;

    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    private void Awake()
    {
      if (TryGetComponent(out _spriteRenderer))
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

      _mainCamera = Camera.main;
    }

    void Start()
    {
      ScaleBackground();
    }

    private void ScaleBackground()
    {
      if (_spriteRenderer == null || _mainCamera == null) return;

      float spriteWidth = _spriteRenderer.sprite.bounds.size.x;
      float spriteHeight = _spriteRenderer.sprite.bounds.size.y;
      float cameraHeight = 2f * _mainCamera.orthographicSize;
      float cameraWidth = cameraHeight * _mainCamera.aspect;

      float scaleX = cameraWidth / spriteWidth + _scaleOffset.x;
      float scaleY = cameraHeight / spriteHeight + _scaleOffset.y;
      float scale = Mathf.Max(scaleX, scaleY);

      transform.localScale = new Vector3(scale, scale, 1f);
      transform.position = new Vector3(transform.position.x, transform.position.y, _zPosition);
    }
  }
}
