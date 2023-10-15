using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{
    private readonly Vector3[] scoreTextPosition = {
        new(0, 0, 0),
        new(0, 0, 0)
        };
    private readonly Vector3[] restartButtonPosition = {
        new(0, 0, 0),
        new(0, 0, 0)
    };
    //UI
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI gameOverText;
    public CanvasRenderer image;

    // Unity Events
    public UnityEvent gameRestart;

    void Awake()
    {
        gameOverText.enabled = false;
        pauseText.enabled = false;
        scoreTextPosition[0] = scoreText.transform.localPosition;
        restartButtonPosition[0] = restartButton.transform.localPosition;
        scoreTextPosition[1] = gameOverText.transform.localPosition + new Vector3(0.0f, -90.0f, 0.0f);
        restartButtonPosition[1] = gameOverText.transform.localPosition + new Vector3(0.0f, -210.0f, 0.0f);
        image.SetAlpha(0.0f);

        // other instructions
        // subscribe to events
        // GameManager.instance.gameStart.AddListener(GameStart);
        // GameManager.instance.gameOver.AddListener(GameOver);
        // GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);

        // GameManager.instance.gamePause.AddListener(GamePause);
        // GameManager.instance.gamePlay.AddListener(GamePlay);

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GamePause()
    {
        pauseText.enabled = true;
    }

    public void GamePlay()
    {
        pauseText.enabled = false;
    }
    public void GameStart()
    {
        // hide gameover panel
        pauseText.enabled = false;
        image.SetAlpha(0.0f);
        gameOverText.enabled = false;
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.transform.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        image.SetAlpha(10.0f);
        gameOverText.enabled = true;
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.transform.localPosition = restartButtonPosition[1];
    }
}
