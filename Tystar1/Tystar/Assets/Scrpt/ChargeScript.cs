using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeScript : MonoBehaviour
{
    [SerializeField] Image Char;

    [SerializeField] private float MaxCharge = 150f;
    private float currentCharge = 0f;

    // 外部参照用（Lightning側など）
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

    // ─────────────────────────
    // チャージ加減算（tekiScript から呼ばれる）
    // ─────────────────────────
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

    // ─────────────────────────
    // Enterで発動（チャージMax必須）
    // ─────────────────────────
    public void EntarWithCallback(GameObject target, EnemySpponScript appearScript)
    {
        // チャージ未完了なら終了
        if (currentCharge < MaxCharge) return;

        // Enterが押された瞬間のみ
        if (!Input.GetKeyDown(KeyCode.Return)) return;

        // 雷を発動
        Lightning lightning = FindObjectOfType<Lightning>();
        if (lightning != null)
        {
            lightning.StartChain();
        }

        // 通常攻撃を併用したい場合だけ使う
        // ForceDefeatEnemy(target, appearScript);

        // チャージ消費
        currentCharge = 0f;
        UpdateGauge();
    }

    // ─────────────────────────
    // 雷・即死用（Lightning専用）
    // ─────────────────────────
    public void ForceDefeatEnemy(GameObject target, EnemySpponScript appearScript)
    {
        if (target == null) return;

        tekiScript enemy = target.GetComponent<tekiScript>();
        if (enemy != null)
        {
            enemy.OnDefeat();
        }

        if (appearScript != null)
        {
            appearScript.ReportEnemyDefeated(100);
        }

        Destroy(target);
    }

    // ─────────────────────────
    // UI更新
    // ─────────────────────────
    void UpdateGauge()
    {
        if (Char != null)
        {
            Char.fillAmount = currentCharge / MaxCharge;
        }
    }
}
