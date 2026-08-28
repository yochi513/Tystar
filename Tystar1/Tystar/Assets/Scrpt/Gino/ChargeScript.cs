using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>敵ごとの入力チャージ、連鎖開始、撃破通知を担当する。</summary>
public class ChargeScript : MonoBehaviour
{
    [SerializeField] Image Char;
    [SerializeField] private float MaxCharge = 150f;
    private float currentCharge = 0f;

    public float CurrentCharge => currentCharge;
    public float MaxChargeValue => MaxCharge;

    public enum Selection
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven
    }

    public Selection Chargetime = Selection.Three;

    void Awake()
    {
        SelectCharge();
        UpdateGauge();
    }

    void SelectCharge()
    {
        switch (Chargetime)
        {
            case Selection.Zero: MaxCharge = 1f; break;
            case Selection.One: MaxCharge = 50f; break;
            case Selection.Two: MaxCharge = 100f; break;
            case Selection.Three: MaxCharge = 150f; break;
            case Selection.Four: MaxCharge = 200f; break;
            case Selection.Five: MaxCharge = 250f; break;
            case Selection.Six: MaxCharge = 350f; break;
            case Selection.Seven: MaxCharge = 500f; break;
        }
    }

    public void Tystar(int amount)
    {
        SelectCharge();

        if (amount > 0)
            currentCharge += 1f;
        else
            currentCharge -= 1f;

        currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
        UpdateGauge();
    }

    public void EntarWithCallback(GameObject target, EnemySpponScript appearScript,HardEnemySpawnScript hardEnemy)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;

        Lightning lightning = FindObjectOfType<Lightning>();
        if (lightning != null && lightning.CanStartChain())
        {
            lightning.StartChain();
            currentCharge = 0f;
            UpdateGauge();
        }
    }
    public void EntarCallback(GameObject target, EnemySpponScript appearScript, HardEnemySpawnScript hardEnemy)
    {
        if (currentCharge==MaxCharge)
        { if (Input.GetKeyDown(KeyCode.Return))
            {
               
                ForceDefeatEnemy(target, appearScript, hardEnemy);
            }
        }
    }

    // ★ 唯一の撃破処理入口
    public void ForceDefeatEnemy(GameObject target, EnemySpponScript appearScript, HardEnemySpawnScript hardEnemy)
    {
        // 破壊処理をここへ集約し、スコア・団子連携・エフェクトの漏れを防ぐ。
        if (target == null) return;

        tekiScript enemy = target.GetComponent<tekiScript>();
        if (enemy == null) return;

       // 二重撃破防止
if (!enemy.TryDefeat()) return;

        // ① 連動オブジェクト通知
        if (enemy.dango != null)
        {
            enemy.dango.OnDestroyed();
        }

        // ② 撃破エフェクト
        if (enemy.DefeatEffectPrefab != null)
        {
            Instantiate(
                enemy.DefeatEffectPrefab,
                target.transform.position,
                Quaternion.identity
            );
        }

        // ③ スポーン管理へ報告
        if (appearScript != null)
        {
            appearScript.ReportEnemyDefeated(100);
        }
        else if (hardEnemy != null)
        {
            hardEnemy.ReportEnemyDefeated();
        }
       
        // ④ 敵削除
        Destroy(target);
    }

    void UpdateGauge()
    {
        if (Char != null)
        {
            Char.fillAmount = currentCharge / MaxCharge;
        }
    }
}
