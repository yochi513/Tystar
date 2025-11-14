using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float bossTime = 5f;


    void Start()
    {
        StartCoroutine(BossTimer());
    }
    // Update is called once per frame
    void Update()
    {


    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);
        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            Debug.Log("Boss戦終了！元のシーンに戻ります");
            SceneManager.LoadScene(staticScript.LastSceneName);
        }
        else
        {
            Debug.LogWarning("戻るシーン情報がありません");
        }
    }

}