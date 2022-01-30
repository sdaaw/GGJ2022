using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameDifficultyScaler = 1;

    public float score = 0;

    public Text scoreText;

    private bool _gameOver = false;

    public bool IsGameOver()
    {
        return _gameOver;
    }

    private void Update()
    {
        if (_gameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            return;
        }
            

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

    public void GameOver()
    {
        _gameOver = true;
        //Display final score
        //restart
    }
}
