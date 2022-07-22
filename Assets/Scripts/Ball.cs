using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BallState {
    Static,
    Dynamic
}

public class Ball : MonoBehaviour {
    private Vector2 _directionOfMovement;
    private float _speedOfMovement;
    private BallState _ballState;
    private CircleCollider2D _ballCollider;
    private float _radius;
    private Collider2D _lastOverlapedCollider;

    void Start() {
        _ballState = BallState.Static;
        _ballCollider = GetComponent<CircleCollider2D>();
        _radius = transform.localScale.x * _ballCollider.radius;
    }

    private void Update() {
        if (_ballState == BallState.Static) return;

        transform.Translate(_directionOfMovement * _speedOfMovement * Time.deltaTime);
    }

    void FixedUpdate() {
        if (_ballState == BallState.Static) return;

        Collider2D overlapCollider = Physics2D.OverlapCircle(transform.position, _radius);
        Debug.DrawLine(transform.position, transform.position + (Vector3)_directionOfMovement * _radius, Color.red);
        if (overlapCollider)
            if (!_lastOverlapedCollider || _lastOverlapedCollider != overlapCollider)
                ReflectionCalculation(overlapCollider);
        _lastOverlapedCollider = overlapCollider;
    }

    private void ReflectionCalculation(Collider2D collider) {
        Table table = collider.GetComponent<Table>();
        if (table) {
            _speedOfMovement *= table.Bounce;

            _directionOfMovement = new Vector2(_directionOfMovement.x, -_directionOfMovement.y);
            if (Physics2D.OverlapCircle((Vector2)transform.position + _directionOfMovement, _radius * 0.95f)) {
                _directionOfMovement = new Vector2(-_directionOfMovement.x, -_directionOfMovement.y);
            }
        }

        Ball otherBall = collider.GetComponent<Ball>();
        if (otherBall) {

        }
    }

    [ContextMenu("Setup")]
    public void Setup(/*float speedOfMovement, Vector2 directionOfMovement*/) {
        // _speedOfMovement = speedOfMovement;
        // _directionOfMovement = directionOfMovement.normalized;
        _speedOfMovement = 3f;
        _directionOfMovement = new Vector2(-0.5f, 1f).normalized;
        _ballState = BallState.Dynamic;
        _ballCollider.enabled = false;
    }
}
