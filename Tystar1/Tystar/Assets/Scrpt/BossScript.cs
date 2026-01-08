using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float bossTime = 5f;
    [SerializeField] private float damagePerCharge = 0.1f;
    [SerializeField] private float damageInterval = 0.2f; // ダメージ&アニメーション間隔

    private Animator animator;
    private float lastDamageTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.BossAttack;
        Debug.Log("ボス戦開始! ステート: " + PlayerStateScript.CurrentState);
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
            if (staticScript.SaveCh > 0 && Time.time >= lastDamageTime + damageInterval)
            {
                ApplyDamage();
            }
        }
    }

    private void ApplyDamage()
    {
        // ダメージ処理
        float damage = damagePerCharge;
        HP -= damage;
        lastDamageTime = Time.time;

        // 被弾アニメーション再生(連続再生)
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        Debug.Log($"ボスにダメージ! 残りHP: {HP}");

        // ボス撃破チェック
        if (HP <= 0)
        {
            OnBossDefeated();
        }
    }

    private void OnBossDefeated()
    {
        Debug.Log("ボスを倒した!");

        // 撃破アニメーションがあれば再生
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // 少し待ってから削除(撃破アニメーションを見せるため)
        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            Debug.Log("Boss戦終了! 元のシーンに戻ります");
            SceneManager.LoadScene(staticScript.LastSceneName);
        }
        else
        {
            Debug.LogWarning("戻るシーン情報がありません");
        }
    }
}