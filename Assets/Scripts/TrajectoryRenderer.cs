using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour {
    [SerializeField] private Transform _line;
    [SerializeField] private Transform _selection;
    [SerializeField] private Transform _ghostBall;

    public void ShowTrajectory(Vector2 origin, Vector2 direction, Quaternion cueAngle, float ballRadius) {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 10f);

        if (hit) {
            _ghostBall.gameObject.SetActive(true);
            Vector2 ghostBallPosition = origin + direction * (hit.distance - ballRadius);
            _ghostBall.position = ghostBallPosition;

            if (hit.collider.GetComponent<Ball>()) {
                _selection.gameObject.SetActive(true);
                _selection.position = hit.collider.transform.position;
            } else {
                _selection.gameObject.SetActive(false);
            }

            float yLineScale = hit.distance - ballRadius * 2f;
            _line.transform.localScale = new Vector3(_line.transform.localScale.x, yLineScale, _line.transform.localScale.z);
            _line.transform.position = origin + direction * hit.distance / 2f;
            _line.transform.rotation = cueAngle;
        } else {
            _selection.gameObject.SetActive(false);
            _ghostBall.gameObject.SetActive(false);
        }
    }
}
