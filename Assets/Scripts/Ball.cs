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
    private Collider2D _ballCollider;

    void Start() {
        _ballState = BallState.Static;
        _ballCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (_ballState == BallState.Static) return;


    }

    void FixedUpdate() {
        if (_ballState == BallState.Static) return;

        transform.Translate(_directionOfMovement * _speedOfMovement * Time.fixedDeltaTime);
        Collider2D overlapCollider = Physics2D.OverlapCircle(transform.position, 0.3625f);
        if (overlapCollider)
            ReflectionCalculation(overlapCollider);
    }

    private void ReflectionCalculation(Collider2D collider) {
        Table table = collider.GetComponent<Table>();
        if (table) {
            _speedOfMovement *= table.Bounce;


            _directionOfMovement = new Vector2(_directionOfMovement.y, -_directionOfMovement.x);
            Debug.Log(_speedOfMovement);
            return;
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
        _directionOfMovement = new Vector2(0.5f, 1f).normalized;
        _ballState = BallState.Dynamic;
        _ballCollider.enabled = false;
    }
}
