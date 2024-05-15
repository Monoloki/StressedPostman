using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private Text pointsLabel;
    [SerializeField] private Text timeLabel;

    private int _points = 0;
    private float _timeLeft = 90f; // in seconds
    private bool gameActive = false;

    [SerializeField] private Material[] symbolMaterials;


    public Material GetSymbolMaterial(ISymbol symbol) {
        return symbolMaterials[(int)symbol];
    }

    private void Start() {
        StartGame();
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
    }

    public void VipParcelExpired() {
        points -= 30;
        timeLeft -= 30;
    }

    public void StartGame() {
        timeLeft = 90f;
        gameActive = true;
        StartCoroutine(RunStopwatch());
    }

    public void StopGame() {
        gameActive = false;
    }

    IEnumerator RunStopwatch() {
        while (gameActive) {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0) {
                timeLeft = 0;
                gameActive = false;
                EndGame();
            }

            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft % 60F);

            timeLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds) + " :TIME";
            yield return null;
        }
    }

    public void EndGame() {
        Debug.Log("End of Game");
    }

}
