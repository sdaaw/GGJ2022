using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameDifficultyScaler = 1;

    public float score = 0;

    public Text scoreText;

    private void Update()
    {
        RotateLevel.speedIncrease = gameDifficultyScaler;

        AddScore(Time.deltaTime * 10);

        UpdateScoreText();
    }

    public void AddScore(float amount)
    {
        score += amount * gameDifficultyScaler;
    }

    public void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = score.ToString("F2");
        }
    }
}
