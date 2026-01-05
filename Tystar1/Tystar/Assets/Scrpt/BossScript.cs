using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float bossTime = 5f;
    [SerializeField] private float damagePerCharge = 0.1f;

    // ★追加
    private Animator animator;

    void Start()
    {
        // ★アニメーターを取得
        animator = GetComponent<Animator>();

        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.BossAttack;
        Debug.Log("ボス戦開始！ステート: " + PlayerStateScript.CurrentState);
        staticScript.SaveCh = 50f;
        StartCoroutine(BossTimer());
    }

    void Update()
    {
        CheckBossDamage();
    }

    private void CheckBossDamage()
    {
        if (PlayerStateScript.CurrentState == PlayerStateScript.PlayerState.BossAttack
        && Input.GetKey(KeyCode.Return))
        {
            if (staticScript.SaveCh > 0)
            {
                // ★被弾アニメーション再生
                if (animator != null)
                {
                    animator.SetBool("IsHit", true); // Bool型パラメータを使用
                }

                float damage = damagePerCharge;
                HP -= damage;

                if (HP <= 0)
                {
                    OnBossDefeated();
                }
            }
        }
        else
        {
            // ★キーを離したらアニメーション停止
            if (animator != null)
            {
                animator.SetBool("IsHit", false);
            }
        }
    }

    private void OnBossDefeated()
    {
        Debug.Log("ボスを倒した!");
        Destroy(gameObject);
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);
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