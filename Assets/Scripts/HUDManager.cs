using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    private Vector3[] scoreTextPosition = {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0)
    };
    //UI
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public CanvasRenderer image;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.enabled = false;
        scoreTextPosition[0] = scoreText.transform.localPosition;
        restartButtonPosition[0] = restartButton.transform.localPosition;
        scoreTextPosition[1] = gameOverText.transform.localPosition + new Vector3(0.0f, -90.0f, 0.0f);
        restartButtonPosition[1] = gameOverText.transform.localPosition + new Vector3(0.0f, -210.0f, 0.0f);
        image.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
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
