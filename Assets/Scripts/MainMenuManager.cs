using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource letsRoll;
    // Start is called before the first frame update
    void Start()
    {

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
        yield return new WaitUntil(() => letsRoll.isPlaying == false);
        Debug.Log("Load world_1-1");
        SceneManager.LoadSceneAsync("world_1-1", LoadSceneMode.Single);
    }

}

