using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottonScript : MonoBehaviour
{
    [SerializeField] Button Retry;
    [SerializeField] Button BacktoMainMenu;
    public UItextScript UIT;
    public BossHPGaugeManager BossHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void retry()
    {
        SceneManager.LoadScene("MainScene");
        UIT.aiu(-999999999);
        BossHP.HPMAX(1500);

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
