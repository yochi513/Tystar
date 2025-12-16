using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float bossTime = 5f;
    [SerializeField] private float damagePerCharge = 0.1f; // CH 0.1消費ごとのダメージ量
    private Animator anim;//アニメーション
    private bool isDead = false; // ← 多重再生防止

    void Start()
    {

        anim = GetComponent<Animator>();

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
               
                // ★ 被弾アニメーション
                if (anim != null)
                {
                    anim.SetTrigger("Hit");
                }

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
        if (isDead) return;   // ★ 二重再生防止
        isDead = true;

        Debug.Log("ボスを倒した!");
        // ボス撃破時の処理（エフェクトなど）
        // TODO: 勝利演出やシーン遷移

        // 死亡アニメーション
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }
        // アニメーション再生後に消す
        Destroy(gameObject, 1.5f);//スパル
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