using UnityEngine;

public class Table : MonoBehaviour {
    [Header("Physics Parameters")]
    [Range(0.01f, 1f)][SerializeField] private float _bounce;
    [SerializeField] private float _friction;

    [Header("Billiard Cue")]
    [SerializeField] private Cue _cue;

    [Header("Game Manager")]
    [SerializeField] private GameManager _gameManager;

    public static Table Instance;

    void Start() {
        if (Instance == null)
            Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != _cue.WhiteBall.gameObject) {
            Destroy(other.gameObject);
            _gameManager.UpdateBallNumber();
        } else {
            _cue.WhiteBall.Setup(0f, Vector2.zero);
            _cue.WhiteBall.transform.position = Vector2.zero;
        }
    }

    public float Bounce { get => _bounce; }
    public float Friction { get => _friction; }
}
