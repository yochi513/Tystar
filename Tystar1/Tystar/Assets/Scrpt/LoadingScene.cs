using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject OUTUI;
   

   
    public void LoadNextScene()
    {
        OUTUI.SetActive(false);
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
