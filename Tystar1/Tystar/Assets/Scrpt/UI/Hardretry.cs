using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Hardモード用のリトライ・難易度選択へ戻るボタン処理。</summary>
public class Hardretry : MonoBehaviour
{
    [SerializeField] Button Retry;
    [SerializeField] Button BacktoMainMenu;

public void retry()
    {
        SceneManager.LoadScene("HardScene");
    }
    public void MainSelect()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
