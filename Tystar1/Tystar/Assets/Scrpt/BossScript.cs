using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float bossTime = 5f;
    [SerializeField] private float damagePerCharge = 0.1f; // CH 0.1消費ごとのダメージ量


    void Start()
    {
        // ボス戦開始時にステートをBossAttackに変更　るい追加
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.BossAttack;

        Debug.Log("ボス戦開始！ステート: " + PlayerStateScript.CurrentState);

        // CHゲージを満タンにする（50が最大値）
        staticScript.SaveCh = 50f;


        StartCoroutine(BossTimer());
    }
    // Update is called once per frame
    void Update()
    {
        CheckBossDamage(); //   るい追加
    }

    
    private void CheckBossDamage()//るい追加 
    {
        // ボスフェーズかつエンターキー長押し中
        if (PlayerStateScript.CurrentState == PlayerStateScript.PlayerState.BossAttack
            && Input.GetKey(KeyCode.Return))
        {
            // CHゲージが残っている場合のみダメージ
            if (staticScript.SaveCh > 0)
            {
                // 1フレームあたりのダメージ
                float damage = damagePerCharge;
                HP -= damage;

                // HPが0以下になったら倒す
                if (HP <= 0)
                {
                    OnBossDefeated();
                }
            }
        }
    }

    private void OnBossDefeated()
    {
        Debug.Log("ボスを倒した!");
        // ボス撃破時の処理（エフェクトなど）
        // TODO: 勝利演出やシーン遷移
        Destroy(gameObject);//スパル
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);

        // ボス戦終了時にステートを元に戻す　るい追加
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

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