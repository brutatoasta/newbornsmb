
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    public int nextSceneIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // save intended destination scene in gamemanager
            GameManager.instance.nextScene = nextSceneIndex;
            // go to loading screen
            SceneManager.LoadSceneAsync(
                GameConstants.sceneNames[
                    (int)GameConstants.ArrayIndex.loading_screen
                ],
                LoadSceneMode.Single);
        }
    }
    // public void MainMenu()
    // {
    //     Debug.Log("Load Main Menu");
    //     SceneManager.LoadSceneAsync(mainMenuName, LoadSceneMode.Single);

    // }
}