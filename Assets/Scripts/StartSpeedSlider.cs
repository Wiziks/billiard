using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartSpeedSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    [Header("Billiard Cue")]
    [SerializeField] private Cue _cue;

    [Header("Visual Elements")]
    [SerializeField] private Gradient _speedIndicator;
    [SerializeField] private RectTransform _cueImage;
    [SerializeField] private Image _background;

    private float _startYImagePosition;
    private float _endYImagePosition;

    private float _fraction;

    private void Start() {
        _startYImagePosition = _cueImage.anchoredPosition.y;
        _endYImagePosition = _startYImagePosition - _background.rectTransform.sizeDelta.y * 0.95f;

        _background.color = _speedIndicator.Evaluate(0);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData) {
        float y = Mathf.Clamp(Input.mousePosition.y - (Screen.height - _background.rectTransform.sizeDelta.y) / 2f, _endYImagePosition, _startYImagePosition);
        _cueImage.anchoredPosition = new Vector2(_cueImage.anchoredPosition.x, y);

        _fraction = 1f - y / _startYImagePosition;

        _background.color = _speedIndicator.Evaluate(1f - y / _startYImagePosition);
    }

    public void OnPointerUp(PointerEventData eventData) {
        _cue.Hit(_fraction);

        _cueImage.anchoredPosition = new Vector2(_cueImage.anchoredPosition.x, _startYImagePosition);
        _background.color = _speedIndicator.Evaluate(0);

        gameObject.SetActive(false);
    }
}
