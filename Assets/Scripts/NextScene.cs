
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    public string nextSceneName;
    public string mainMenuName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Change scene!");
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        }
    }
    public void MainMenu()
    {
        Debug.Log("Load Main Menu");
        SceneManager.LoadSceneAsync(mainMenuName, LoadSceneMode.Single);

    }
}