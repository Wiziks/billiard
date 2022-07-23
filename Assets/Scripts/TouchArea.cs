using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    [SerializeField] private Cue _cue;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (_cue.WhiteBall.BallState == BallState.Dynamic) return;

        _trajectoryRenderer.gameObject.SetActive(true);
        _cue.gameObject.SetActive(true);
        _cue.transform.position = _cue.WhiteBall.transform.position;
        _trajectoryRenderer.transform.position = _cue.transform.position;
        SetupCue();
    }

    public void OnDrag(PointerEventData eventData) {
        if (_cue.WhiteBall.BallState == BallState.Dynamic) return;

        SetupCue();
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (_cue.WhiteBall.BallState == BallState.Dynamic) return;

        _cue.WhiteBall.Setup(_cue.ImpactForce, _cue.transform.up);
        _cue.gameObject.SetActive(false);
        _trajectoryRenderer.gameObject.SetActive(false);
    }

    private void SetupCue() {
        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = mouseWorldPosition - (Vector2)_cue.WhiteBall.transform.position;

        float zAngle = Mathf.Atan(Mathf.Abs(delta.x / delta.y)) * 180 / Mathf.PI;

        if (_cue.WhiteBall.transform.position.y < mouseWorldPosition.y) zAngle = 180f - zAngle;
        if (_cue.WhiteBall.transform.position.x > mouseWorldPosition.x) zAngle *= -1f;

        _cue.transform.rotation = Quaternion.Euler(0, 0, zAngle);

        _trajectoryRenderer.ShowTrajectory(_cue.transform.position, _cue.transform.up, _cue.transform.rotation, _cue.WhiteBall.Radius);
    }
}
