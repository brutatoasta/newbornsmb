using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent gamePause;
    public UnityEvent gamePlay;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent marioDeath;
    public IntVariable gameScore;
    public bool isPaused = false;
    void Start()
    {
        Debug.Log("setvalue0");
        gameScore.SetValue(0);
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GamePause()
    {
        Time.timeScale = 0;
        isPaused = true;
        // pause audiolistener
        AudioListener.pause = true;
        gamePause.Invoke();

    }
    public void GamePlay()
    {
        Time.timeScale = 1;
        isPaused = false;
        AudioListener.pause = false;
        gamePlay.Invoke();
    }
    public void GameRestart()
    {
        // reset score
        gameScore.SetValue(0);
        SetScore(gameScore.Value);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;

        isPaused = false;
        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;
    }
    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(gameScore.Value);
    }
    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore(gameScore.Value);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void ResetHighScore()
    {
        gameScore.ResetHighestValue();
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
    public void MarioDeath()
    {
        marioDeath.Invoke();
    }

}