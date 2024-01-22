using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    private bool _isGameOver = false;

    public bool isGameOver 
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            _isGameOver = value;
        }
    }

    public GameObject gameOverUI;

    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void RetryGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Debug.Log("Retry button pressed");
        Time.timeScale = 1f;
        isGameOver = false;
        AudioListener.pause = false;
    }
}
