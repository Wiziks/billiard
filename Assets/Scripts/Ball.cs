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


        _speedOfMovement -= Table.Instance.Friction * Time.deltaTime * 9.81f;

        if (_speedOfMovement <= 0) {
            _ballState = BallState.Static;
        }
    }

    void FixedUpdate() {
        if (_ballState == BallState.Static) return;

        transform.Translate(_directionOfMovement * _speedOfMovement * Time.fixedDeltaTime);

        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (Collider2D overlapCollider in allColliders) {
            if (overlapCollider != _ballCollider) {
                if (!_lastOverlapCollider || _lastOverlapCollider != overlapCollider)
                    ReflectionCalculation(overlapCollider);

                _lastOverlapCollider = overlapCollider;
            }
        }

    }

    private void ReflectionCalculation(Collider2D collider) {
        Table table = collider.GetComponent<Table>();
        if (table) {
            _speedOfMovement *= table.Bounce;

            _directionOfMovement = new Vector2(_directionOfMovement.x, -_directionOfMovement.y);
            if (Physics2D.OverlapCircle((Vector2)transform.position + _directionOfMovement, _radius * 0.99f)) {
                _directionOfMovement = new Vector2(-_directionOfMovement.x, -_directionOfMovement.y);
            }

            return;
        }

        Ball otherBall = collider.GetComponent<Ball>();
        if (otherBall) {
            Vector2 otherBallDirection = otherBall.transform.position - transform.position;

            float speedKoeficient = Vector2.Angle(_directionOfMovement, otherBallDirection) / 90f;
            Debug.Log(speedKoeficient);
            otherBall.Setup(_speedOfMovement * (1f - speedKoeficient), otherBallDirection);
            _speedOfMovement *= speedKoeficient;

            _directionOfMovement = new Vector2(otherBallDirection.y, -otherBallDirection.x);
            if (Physics2D.OverlapCircle((Vector2)transform.position + _directionOfMovement, _radius * 0.99f)) {
                _directionOfMovement = new Vector2(-otherBallDirection.y, -otherBallDirection.x);
            }

        }
    }

    public void Setup(float speedOfMovement, Vector2 directionOfMovement) {
        _speedOfMovement = speedOfMovement;
        _directionOfMovement = directionOfMovement.normalized;
        _ballState = BallState.Dynamic;
    }

    public float Radius { get => _radius; }
    internal BallState BallState { get => _ballState; }
}
