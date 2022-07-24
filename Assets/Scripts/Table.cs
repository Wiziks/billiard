using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    [Range(0.01f, 1f)][SerializeField] private float _bounce;
    [SerializeField] private float _friction;
    public static Table Instance;

    void Start() {
        if (Instance == null)
            Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }

    public float Bounce { get => _bounce; }
    public float Friction { get => _friction; }
}
