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

    public GameObject gameOverScreen;
    public Text gameOverScoreText;

    public GameObject gameWonScreen;
    public Text gameWonScroeText;

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
                ReloadLevel();
            }

            return;
        }
            

        RotateLevel.speedIncrease = gameDifficultyScaler;

        AddScore(Time.deltaTime * 10);

        UpdateScoreText();
    }
    
    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void GoToMenu()
    {
        Application.LoadLevel(0);
    }

    public void AddScore(float amount)
    {
        score += amount * gameDifficultyScaler;
    }

    public void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = score.ToString("F0");
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        SoundManager.PlayASource("Lose");
        gameOverScreen.SetActive(true);
        gameOverScoreText.text = "Final Score:" + score.ToString("F0");
        //Display final score
        //restart
    }

    public void GameWon()
    {
        _gameOver = true;
        SoundManager.PlayASource("Win");
        gameWonScreen.SetActive(true);
        gameWonScroeText.text = "Final Score:" + score.ToString("F0");
    }
}
