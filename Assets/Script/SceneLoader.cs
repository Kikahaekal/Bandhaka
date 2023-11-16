using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public float delayTime = 2.5f;

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadScreenAsync(levelIndex));
        Time.timeScale = 1f;
    }

    IEnumerator LoadScreenAsync(int levelIndex)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(delayTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while(!operation.isDone)
        {
            yield return null;
        } 
    }
}
