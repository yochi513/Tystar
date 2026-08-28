using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
