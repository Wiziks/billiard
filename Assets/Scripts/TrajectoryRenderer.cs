using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour {
    [Header("Trajectory Elements")]
    [SerializeField] private Transform _line;
    [SerializeField] private Transform _lineToBall;
    [SerializeField] private Transform _bounceLine;
    [SerializeField] private Transform _selection;
    [SerializeField] private Transform _collisionPosition;

    [Header("Ball Tag")]
    [SerializeField] private string _ballTagName;

    public void ShowTrajectory(Vector2 origin, Vector2 direction, Quaternion cueAngle, float ballRadius) {
        _collisionPosition.gameObject.SetActive(true);

        /*Calculation of collision information*/
        Vector2 offset = new Vector2(direction.y, -direction.x);
        RaycastHit2D hit = Physics2D.Raycast(origin + direction * ballRadius * 1.01f, direction, 50f);
        RaycastHit2D hitLeft = Physics2D.Raycast(origin + offset * ballRadius + direction * 0.01f, direction, 50f);
        RaycastHit2D hitRight = Physics2D.Raycast(origin - offset * ballRadius + direction * 0.01f, direction, 50f);

        if (hit && hit.collider.tag == _ballTagName) {
            if (hitLeft && hitLeft.collider.tag == _ballTagName) {
                if (hit.distance > hitLeft.distance) hit = hitLeft;
            }
        } else {
            if (hitLeft && hitLeft.collider.tag == _ballTagName) hit = hitLeft;
        }

        if (hit && hit.collider.tag == _ballTagName) {
            if (hitRight && hitRight.collider.tag == _ballTagName) {
                if (hit.distance > hitRight.distance) hit = hitRight;
            }
        } else {
            if (hitRight && hitRight.collider.tag == _ballTagName) hit = hitRight;
        }

        if (!hit) return;

        /*Collision calculation if the ball collided with the table*/
        if (hit.collider.tag != _ballTagName) {
            _selection.gameObject.SetActive(false);
            _lineToBall.gameObject.SetActive(false);
            _bounceLine.gameObject.SetActive(false);

            hit = Physics2D.Raycast(origin + direction * ballRadius * 1.01f, direction, 50f);
            DrawCollision(hit, origin, direction, cueAngle, ballRadius);

            return;
        }

        /*Collision calculation if the ball collided with the another ball*/
        DrawCollision(hit, origin, direction, cueAngle, ballRadius);

        _selection.gameObject.SetActive(true);
        _lineToBall.gameObject.SetActive(true);
        _bounceLine.gameObject.SetActive(true);

        _selection.position = hit.collider.transform.position;

        /*Calculation of the angle of rotation of the line of the further trajectory of the bounced ball*/
        Vector2 delta = hit.collider.transform.position - _collisionPosition.position;

        float zAngleLineToBall = Mathf.Atan(Mathf.Abs(delta.x / delta.y)) * 180 / Mathf.PI;

        if (_collisionPosition.position.y < hit.collider.transform.position.y) zAngleLineToBall = 180f - zAngleLineToBall;
        if (_collisionPosition.position.x > hit.collider.transform.position.x) zAngleLineToBall *= -1f;

        _lineToBall.rotation = Quaternion.Euler(0, 0, zAngleLineToBall);
        _lineToBall.position = _collisionPosition.position;

        /*Calculation of the angle of rotation of the line of the further trajectory of the launched ball*/
        _bounceLine.position = _collisionPosition.position;

        Vector2 equilibriumPoint = (Vector2)hit.collider.transform.position - direction * (ballRadius + _collisionPosition.localScale.y);

        float zAngleBounceLine = zAngleLineToBall + ((equilibriumPoint.y > _collisionPosition.position.y) ? -90f : 90f);
        zAngleBounceLine += hit.collider.transform.position.x > origin.x ? 0f : 180f;
        _bounceLine.rotation = Quaternion.Euler(0, 0, zAngleBounceLine);

        /*Calculation of the speed multiplier of both balls relative to the angles of their trajectories*/
        float yBounceLineScale = Mathf.Abs(cueAngle.eulerAngles.z - zAngleBounceLine);
        while (yBounceLineScale > 180f) yBounceLineScale -= 180f;
        yBounceLineScale = Mathf.Abs(yBounceLineScale - 90f) / 90f;
        _bounceLine.localScale = new Vector3(_bounceLine.localScale.x, yBounceLineScale, _bounceLine.localScale.z);
        _lineToBall.localScale = new Vector3(_lineToBall.localScale.x, 1f - yBounceLineScale, _lineToBall.localScale.z);
    }
    
    private void DrawCollision(RaycastHit2D hit, Vector2 origin, Vector2 direction, Quaternion cueAngle, float ballRadius) {
        _collisionPosition.position = origin + direction * hit.distance;

        float yLineScale = hit.distance - ballRadius;
        _line.localScale = new Vector3(_line.localScale.x, yLineScale, _line.localScale.z);
        _line.position = origin + direction * (hit.distance + ballRadius) / 2f;
        _line.rotation = cueAngle;
    }
}
