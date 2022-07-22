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
    private Collider2D _lastOverlapCollider;

    void Start() {
        _ballState = BallState.Static;
        _ballCollider = GetComponent<CircleCollider2D>();
        _radius = transform.localScale.x * _ballCollider.radius;
    }

    private void Update() {
        if (_ballState == BallState.Static) return;

        transform.Translate(_directionOfMovement * _speedOfMovement * Time.deltaTime);

        _speedOfMovement -= Table.Instance.Friction * Time.deltaTime;

        if (_speedOfMovement <= 0)
            _ballState = BallState.Static;
    }

    void FixedUpdate() {
        if (_ballState == BallState.Static) return;

        Collider2D overlapCollider = Physics2D.OverlapCircle(transform.position, _radius);
        if (overlapCollider)
            if (!_lastOverlapCollider || _lastOverlapCollider != overlapCollider)
                ReflectionCalculation(overlapCollider);

        _lastOverlapCollider = overlapCollider;
    }

    private void ReflectionCalculation(Collider2D collider) {
        Table table = collider.GetComponent<Table>();
        if (table) {
            _speedOfMovement *= table.Bounce;

            _directionOfMovement = new Vector2(_directionOfMovement.x, -_directionOfMovement.y);
            if (Physics2D.OverlapCircle((Vector2)transform.position + _directionOfMovement, _radius * 0.95f)) {
                _directionOfMovement = new Vector2(-_directionOfMovement.x, -_directionOfMovement.y);
            }

            return;
        }

        Ball otherBall = collider.GetComponent<Ball>();
        if (otherBall) {

        }
    }

    public void Setup(float speedOfMovement, Vector2 directionOfMovement) {
        _speedOfMovement = speedOfMovement;
        _directionOfMovement = directionOfMovement.normalized;
        _ballState = BallState.Dynamic;
        _ballCollider.enabled = false;
    }
}
