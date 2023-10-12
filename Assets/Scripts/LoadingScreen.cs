using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private bool loadNext;
    void Start()
    {
        loadNext = true;
        StartCoroutine(
            WaitThenLoadScene(
                GameConstants.sceneNames[
                    GameManager.instance.nextScene
                ]
            )
        );
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator WaitThenLoadScene(string nextSceneName)
    {

        yield return new WaitForSeconds(3);
        if (loadNext)
        {
            GameManager.instance.currentScene = GameManager.instance.nextScene;
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        }

    }
    public void ReturnToMainMenu()
    {
        loadNext = false;
        SceneManager.LoadSceneAsync(
            GameConstants.sceneNames[(int)GameConstants.ArrayIndex.main_menu],
            LoadSceneMode.Single);
    }
}
