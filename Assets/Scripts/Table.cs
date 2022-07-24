using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    [Range(0.01f, 1f)][SerializeField] private float _bounce;
    [SerializeField] private float _friction;
    [SerializeField] private Cue _cue;
    public static Table Instance;

    void Start() {
        if (Instance == null)
            Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != _cue.WhiteBall.gameObject)
            Destroy(other.gameObject);
        else {
            _cue.WhiteBall.Setup(0f, Vector2.zero);
            other.transform.position = Vector2.zero;
        }
    }

    public float Bounce { get => _bounce; }
    public float Friction { get => _friction; }
}
