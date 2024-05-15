using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private Text pointsLabel;
    [SerializeField] private Text timeLabel;

    private int _points = 0;
    private float _timeLeft = 90f; // in seconds
    public bool gameActive = false;

    [SerializeField] private Material[] symbolMaterials;
    [SerializeField] private GameObject summaryScreen;

    [SerializeField] private Spawner spawner;
    [SerializeField] private Boss bossSpawner;
    [SerializeField] private Text summaryPointsLabel;


    public Material GetSymbolMaterial(ISymbol symbol) {
        return symbolMaterials[(int)symbol];
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public float timeLeft {
        get { return _timeLeft; }
        set {
            _timeLeft = value;
        }
    }

    public int points {
        get { return _points; }
        set {
            _points = value;
            pointsLabel.text = $"Points:{points}";
        }
    }

    public void ParcledPlacedCorrectly() {
        points += 10;
        timeLeft += 5;
    }

    public void ParcelPlacedWrongly() {
        timeLeft -= 5;
    }

    public void VipParcledPlacedCorrectly() {
        points += 20;
        timeLeft += 10;
    }

    public void VipParcledPlacedWrongly() {
        timeLeft -= 10;
        if (timeLeft <= 0) {
            timeLeft = 0;
        }
    }

    public void VipParcelExpired() {
        points -= 30;
        timeLeft -= 30;
        if (timeLeft <= 0) {
            timeLeft = 0;
        }
    }

    public void StartGame() {
        timeLeft = 90f;
        gameActive = true;
        StartCoroutine(RunStopwatch());
        bossSpawner.StartBossSpawner();
        spawner.StartRegularSpawner();
    }

    public void StopGame() {
        gameActive = false;
    }

    IEnumerator RunStopwatch() {
        while (gameActive) {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0) {
                timeLeft = 0;
                EndGame();
                gameActive = false;
            }

            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft % 60F);

            timeLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds) + " :TIME";
            yield return null;
        }
    }

    public void EndGame() {
        gameActive = false;
        summaryPointsLabel.text = $"Points: {points}";
        summaryScreen.SetActive(true);
    }

}
