using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void EntarCallback(GameObject target, HardEnemySpawnScript hardEnemy)
    {
        if (currentCharge==MaxCharge)
        { if (Input.GetKeyDown(KeyCode.Return))
                Destroy(target);
        }
    }

    // š —Bˆê‚ÌŒ‚”jˆ—“üŒû
    public void ForceDefeatEnemy(GameObject target, EnemySpponScript appearScript, HardEnemySpawnScript hardEnemy)
    {
        if (target == null) return;

        tekiScript enemy = target.GetComponent<tekiScript>();
        if (enemy == null) return;

       // “ñdŒ‚”j–h~
if (!enemy.TryDefeat()) return;

        // ‡@ ˜A“®ƒIƒuƒWƒFƒNƒg’Ê’m
        if (enemy.dango != null)
        {
            enemy.dango.OnDestroyed();
        }

        // ‡A Œ‚”jƒGƒtƒFƒNƒg
        if (enemy.DefeatEffectPrefab != null)
        {
            Instantiate(
                enemy.DefeatEffectPrefab,
                target.transform.position,
                Quaternion.identity
            );
        }

        // ‡B ƒXƒ|[ƒ“ŠÇ—‚Ö•ñ
        if (appearScript != null)
        {
            appearScript.ReportEnemyDefeated(100);
        }
        if (hardEnemy != null)
        {
            hardEnemy.ReportEnemy();
            GetComponent<EnemyUIScript>().Count(1);
        }

        // ‡C “Gíœ
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
