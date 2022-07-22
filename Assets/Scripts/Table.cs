using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    [Range(0.01f, 1f)][SerializeField] private float _bounce;


    void Start() {

    }

    void Update() {

    }

    public float Bounce { get => _bounce; }
}
