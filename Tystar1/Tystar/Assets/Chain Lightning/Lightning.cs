using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("雷エフェクト設定")]
    public GameObject lightningPrefab;

    [Header("連鎖設定")]
    [SerializeField] private float chainDelay = 0.2f;
    [SerializeField] private float hitEffectDuration = 0.5f;
    [SerializeField] private float chainEffectDuration = 0.3f;

    [Header("エフェクトのスケール調整")]
    [SerializeField] private bool adjustScale = true;
    [SerializeField] private float baseDistance = 1f; // Prefabが長さ1mの場合

    private List<tekiScript> holdingOrder = new List<tekiScript>();
    public static bool isExecutingChain { get; private set; } = false;

    void Update()
    {
        if (isExecutingChain) return;

        holdingOrder.RemoveAll(enemy => enemy == null);
        tekiScript[] enemies = FindObjectsOfType<tekiScript>();

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            KeyCode key = enemy.assignedKey;

            if (Input.GetKeyDown(key) && !holdingOrder.Contains(enemy))
            {
                holdingOrder.Add(enemy);
            }
            if (Input.GetKeyUp(key) && holdingOrder.Contains(enemy))
            {
                holdingOrder.Remove(enemy);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && holdingOrder.Count >= 1)
        {
            StartCoroutine(ChainLightning());
        }
    }

    IEnumerator ChainLightning()
    {
        isExecutingChain = true;
        List<tekiScript> chainList = new List<tekiScript>(holdingOrder);

        // 1. 始点リストの作成（自分の位置を最初に追加）
        List<Vector3> points = new List<Vector3>();
        points.Add(this.transform.position); // 自分の位置

        foreach (var enemy in chainList)
        {
            if (enemy != null) points.Add(enemy.transform.position);
        }

        holdingOrder.Clear();

        // 2. 連鎖の実行（points[0]が自分、points[1]が1体目）
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 startPos = points[i];
            Vector3 endPos = points[i + 1];

            // 連鎖線を表示（自分→敵1、敵1→敵2...）
            CreateChainEffect(startPos, endPos);

            // 敵への着弾エフェクトと破壊（自分自身[0]はスキップ）
            if (i < chainList.Count)
            {
                tekiScript targetEnemy = chainList[i];
                if (targetEnemy != null)
                {
                    ShowLightningEffect(endPos);

                    if (targetEnemy.charge != null)
                    {
                        yield return new WaitForSeconds(0.05f);
                        targetEnemy.charge.ForceDefeatEnemy(targetEnemy.gameObject, targetEnemy.AppEnemy);
                    }
                }
            }

            yield return new WaitForSeconds(chainDelay);
        }

        isExecutingChain = false;
    }

    void ShowLightningEffect(Vector3 position)
    {
        if (lightningPrefab != null)
        {
            GameObject lightning = Instantiate(lightningPrefab, position, Quaternion.identity);
            Destroy(lightning, hitEffectDuration);
        }
    }

    void CreateChainEffect(Vector3 startPos, Vector3 endPos)
    {
        if (lightningPrefab == null) return;

        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;

        if (distance < 0.1f) return;

        // 生成位置を「2点の中間」にする
        Vector3 centerPos = startPos + (direction * 0.5f);
        GameObject chainEffect = Instantiate(lightningPrefab, centerPos, Quaternion.identity);

        // Z軸を目標に向ける（LookRotationの方が安定します）
        chainEffect.transform.forward = direction.normalized;

        if (adjustScale)
        {
            // Z軸方向に距離を伸ばす
            Vector3 scale = chainEffect.transform.localScale;
            scale.z = distance / baseDistance;
            chainEffect.transform.localScale = scale;
        }

        Destroy(chainEffect, chainEffectDuration);
    }
}