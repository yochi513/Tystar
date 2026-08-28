using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Serialization;

/// <summary>プレイヤーHP、被弾後の無敵時間、ゲームオーバーUIを管理する。</summary>
public class Playerscrpt : MonoBehaviour
{
    [FormerlySerializedAs("PlayerHP")]
    [SerializeField] private int playerHP = 6;
    public bool isInvincible = false;
    public float invincibleTime = 1.5f;
    private bool isGameOver;

    public int PlayerHP => playerHP;
 

    [SerializeField] Canvas Gameover;
    [SerializeField] Canvas MainCanvas;

    void Start()
    {
        // ボスシーンやClear画面では消費せず、元の難易度シーンへ戻った時だけ復元する。
        bool returnedToOriginScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == staticScript.LastSceneName;
        if (staticScript.RestorePlayerHpOnSceneLoad && !staticScript.IsGoingToBoss && returnedToOriginScene)
        {
            playerHP = Mathf.Max(0, staticScript.SavePlayerHP);
            staticScript.RestorePlayerHpOnSceneLoad = false;
        }

        Gameover.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(true);
    }

    void Update()
    {


    }


    public int GetCurrentHP()
    {
        return playerHP;
    }
    public void SetHP(int hp)
    {
        playerHP = Mathf.Max(0, hp);
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        playerHP -= damage;

        StartCoroutine(InvincibleCoroutine());
        if (playerHP <= 0)
        {
            GameOVER();
        }
    }

    
    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime); // 決めた時間待つ
        isInvincible = false;
    }
    
    public void GameOVER()
    {
        if (isGameOver) return;
        isGameOver = true;
        Gameover.gameObject.SetActive(true);
       
        MainCanvas.gameObject.SetActive(false);
    }

}
