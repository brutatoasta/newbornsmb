using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource letsRoll;
    TextMeshProUGUI highscore;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.resetHighscore.AddListener(SetScore);
    }
    void Start()
    {
        highscore = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
        SetScore();
    }

    public void SetScore()
    {
        highscore.text = "TOP- " + GameManager.instance.gameScore.previousHighestValue.ToString("000000");
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void gameStart()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }
    private IEnumerator PlayAudioThenLoadScene()
    {
        letsRoll.PlayOneShot(letsRoll.clip);
        yield return new WaitUntil(() => !letsRoll.isPlaying);

        SceneManager.LoadSceneAsync(
            GameConstants.sceneNames[
                (int)GameConstants.ArrayIndex.loading_screen
                ],
            LoadSceneMode.Single);
    }

}

