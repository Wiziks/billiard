using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour {
    [SerializeField] private Transform _line;
    [SerializeField] private Transform _lineToBall;
    [SerializeField] private Transform _bounceLine;
    [SerializeField] private Transform _selection;
    [SerializeField] private Transform _ghostBall;

    public void ShowTrajectory(Vector2 origin, Vector2 direction, Quaternion cueAngle, float ballRadius) {
        RaycastHit2D hit = Physics2D.Raycast(origin + direction * ballRadius * 1.01f, direction, 10f);

        if (hit) {
            _ghostBall.gameObject.SetActive(true);
            Vector2 ghostBallPosition = origin + direction * hit.distance;
            _ghostBall.position = ghostBallPosition;

            if (hit.collider.GetComponent<Ball>()) {
                _selection.gameObject.SetActive(true);
                _lineToBall.gameObject.SetActive(true);
                _bounceLine.gameObject.SetActive(true);

                _selection.position = hit.collider.transform.position;

                Vector2 delta = hit.collider.transform.position - _ghostBall.position;

                float zAngleLineToBall = Mathf.Atan(Mathf.Abs(delta.x / delta.y)) * 180 / Mathf.PI;

                if (_ghostBall.position.y < hit.collider.transform.position.y) zAngleLineToBall = 180f - zAngleLineToBall;
                if (_ghostBall.position.x > hit.collider.transform.position.x) zAngleLineToBall *= -1f;

                _lineToBall.rotation = Quaternion.Euler(0, 0, zAngleLineToBall);
                _lineToBall.position = _ghostBall.position;

                _bounceLine.position = _ghostBall.position;

                float zAngleBounceLine = zAngleLineToBall + (zAngleLineToBall - cueAngle.eulerAngles.z < -180 ? 90f : -90f);
                _bounceLine.rotation = Quaternion.Euler(0, 0, zAngleBounceLine);

                float leftKoef = zAngleLineToBall;
                if (leftKoef < 0f)
                    leftKoef += 180f;
                else if (leftKoef > 180f)
                    leftKoef -= 180f;

                float rightKoef = cueAngle.eulerAngles.z;
                if (rightKoef < 0f)
                    rightKoef += 180f;
                else if (rightKoef > 180f)
                    rightKoef -= 180f;


                float yBounceLineScale = Mathf.Abs(leftKoef - rightKoef) / 90f;
                _bounceLine.localScale = new Vector3(_bounceLine.localScale.x, yBounceLineScale, _bounceLine.localScale.z);
            } else {
                _selection.gameObject.SetActive(false);
                _lineToBall.gameObject.SetActive(false);
                _bounceLine.gameObject.SetActive(false);
            }

            float yLineScale = hit.distance - ballRadius;
            _line.localScale = new Vector3(_line.localScale.x, yLineScale, _line.localScale.z);
            _line.position = origin + direction * (hit.distance + ballRadius) / 2f;
            _line.rotation = cueAngle;
        } else {
            _selection.gameObject.SetActive(false);
            _ghostBall.gameObject.SetActive(false);
            _lineToBall.gameObject.SetActive(false);
            _bounceLine.gameObject.SetActive(false);
        }
    }
}
