using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int currentScore = 0;
    int highScore = 0;

    [SerializeField] TMP_Text currentScoreText;
    [SerializeField] TMP_Text highScoreText;

    private void Awake() {
        
    }

    public void IncreaseScore(int amount) {
        currentScore += amount;
        UpdateScore();
    }

    void UpdateScore() {
        if (currentScore > highScore) highScore = currentScore;

        currentScoreText.text = currentScore.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void ResetScrore() {
        currentScore = 0;
        UpdateScore();
    }
}
