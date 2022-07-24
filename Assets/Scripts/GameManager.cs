using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _numberOfBalls;
    void Start() {
        _numberOfBalls = FindObjectsOfType<Ball>().Length - 1;
    }

    public void SetTimeScale(float value) {
        Time.timeScale = value;
    }

    public void UpdateBallNumber() {
        _numberOfBalls--;
        _scoreText.text = $"Left: {_numberOfBalls}";
        if (_numberOfBalls == 0) ReloadScene();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
