using UnityEngine;

public class Cue : MonoBehaviour {
    [Header("Max Impact Force")]
    [SerializeField] private float _maxImpactForce;

    [Header("Trajectory Renderer")]
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    [Header("White Ball")]
    [SerializeField] private Ball _whiteBall;

    public void Hit(float fraction) {
        _whiteBall.Setup(fraction * _maxImpactForce, transform.up);
        gameObject.SetActive(false);
        _trajectoryRenderer.gameObject.SetActive(false);
    }

    public float MaxImpactForce { get => _maxImpactForce; }
    public Ball WhiteBall { get => _whiteBall; }
}
