using UnityEngine;

public class Cue : MonoBehaviour {
    [Header("")]
    [SerializeField] private float _maxImpactForce;

    [Header("")]
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    [Header("")]
    [SerializeField] private Ball _whiteBall;

    public void Hit(float fraction) {
        _whiteBall.Setup(fraction * _maxImpactForce, transform.up);
        gameObject.SetActive(false);
        _trajectoryRenderer.gameObject.SetActive(false);
    }

    public float MaxImpactForce { get => _maxImpactForce; }
    public Ball WhiteBall { get => _whiteBall; }
}
