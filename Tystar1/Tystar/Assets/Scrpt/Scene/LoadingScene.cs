using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>ロード画面を表示してから、非同期でメインシーンを読み込む。</summary>
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject OUTUI;
    private bool isLoading;
   

   
    public void LoadNextScene()
    {
        if (isLoading) return;
        isLoading = true;

        OUTUI.SetActive(false);
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
      //  AsyncOperation async2 = SceneManager.LoadSceneAsync("NormalScene");
        while (!async.isDone)
        {
            yield return null;
        }
        //while (!async2.isDone)
        //{
        //    yield return null;
        //}
    }
}
