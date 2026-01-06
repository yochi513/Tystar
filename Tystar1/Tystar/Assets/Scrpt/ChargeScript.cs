using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ChargeScript : MonoBehaviour
{
    [SerializeField] Image Char;
    private float MaxCharge = 500f;
    private float currentCharge = 0f;

    public enum Selection
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7
    }

    public Selection Chargetime = Selection.Three;

    public void select()
    {
        if (Chargetime == Selection.Two)
        {
            MaxCharge = 100f;
        }
        else if (Chargetime == Selection.Three)
        {
            MaxCharge = 150f;
        }
        else if (Chargetime == Selection.Four)
        {
            MaxCharge = 200f;
        }
        else if (Chargetime == Selection.Five)
        {
            MaxCharge = 250f;
        }
        else if (Chargetime == Selection.Six)
        {
            MaxCharge = 350f;
        }
        else if (Chargetime == Selection.Seven)
        {
            MaxCharge = 500f;
        }
        else if (Chargetime == Selection.One)
        {
            MaxCharge = 50f;
        }
        else if (Chargetime == Selection.Zero)
        {
            MaxCharge = 1f;
        }
    }

    // ゲージ加算（敵から呼び出される）
    public void Tystar(int amount)
    {
        select();
        if (amount == 1)
        {
            currentCharge++;
        }
        else
        {
            currentCharge--;
        }
        currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
        UpdateGauge();
    }

    // UI更新
    private void UpdateGauge()
    {
        select();
        if (Char != null)
        {
            Char.fillAmount = currentCharge / MaxCharge;
        }
    }

    // クラスの先頭に追加するフィールド
    [SerializeField] private AudioClip defeatSound; // Inspector で設定する効果音
    private AudioSource audioSource;

    // Start メソッドまたは Awake メソッドに追加
    void Start()
    {
        // AudioSource コンポーネントを取得または追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void EntarWithCallback(GameObject target, EnemySpponScript appearScript)
    {
        select();
        if (currentCharge >= MaxCharge && Input.GetKeyDown(KeyCode.Return))
        {
            // 効果音を最初に再生（敵を消す前）
            if (defeatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(defeatSound, 1.0f);
            }

            // 敵を削除する前にエフェクトを再生
            tekiScript enemyScript = target.GetComponent<tekiScript>();
            if (enemyScript != null)
            {
                enemyScript.OnDefeat();
            }

            if (appearScript != null)
            {
                appearScript.ReportEnemyDefeated(100); // 報告
            }
            Destroy(target);
            currentCharge = 0f;
            UpdateGauge();
        }
    }

    // ★ 連鎖雷専用：チャージ条件を無視して即座に敵を倒す
    public void ForceDefeatEnemy(GameObject target, EnemySpponScript appearScript)
    {
        if (target == null) return;

        // 敵を削除する前にエフェクトを再生
        tekiScript enemyScript = target.GetComponent<tekiScript>();
        if (enemyScript != null)
        {
            enemyScript.OnDefeat();
        }

        if (appearScript != null)
        {
            appearScript.ReportEnemyDefeated(100); // 報告
        }

        Destroy(target);

        // チャージはリセットしない（連鎖雷はチャージを消費しない仕様の場合）
        // もしチャージを消費させたい場合は以下をコメント解除
        // currentCharge = 0f;
        // UpdateGauge();
    }
}
//10秒=500F
//300fで6秒くらい
//250fで5秒くらい
//200fで4秒くらい
//150fで3秒くらい
//100fで2秒くらい
//50f で1秒くらい