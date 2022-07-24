using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour {
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private float _maxImpactForce;
    [SerializeField] private Ball _whiteBall;

    public void Hit(float fraction) {
        _whiteBall.Setup(fraction * _maxImpactForce, transform.up);
        gameObject.SetActive(false);
        _trajectoryRenderer.gameObject.SetActive(false);
    }

    public float MaxImpactForce { get => _maxImpactForce; }
    public Ball WhiteBall { get => _whiteBall; }
}
