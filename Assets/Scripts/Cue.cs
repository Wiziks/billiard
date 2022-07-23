using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour {
    [SerializeField] private float _impactForce;
    [SerializeField] private Ball _whiteBall;

    public float ImpactForce { get => _impactForce; }
    public Ball WhiteBall { get => _whiteBall; }
}
