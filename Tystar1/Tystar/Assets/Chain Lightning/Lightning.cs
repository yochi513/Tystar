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
    [SerializeField] private float baseDistance = 1f;

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
    }

    // ★ 外部から呼ぶ専用メソッド（ChargeScriptから呼ばれる前にチェック）
    public bool CanStartChain()
    {
        if (isExecutingChain) return false;
        if (holdingOrder.Count == 0) return false;

        // チャージが満タンの敵が1体でもいるかチェック
        foreach (var enemy in holdingOrder)
        {
            if (enemy != null && enemy.charge != null)
            {
                if (enemy.charge.CurrentCharge >= enemy.charge.MaxChargeValue)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void StartChain()
    {
        if (isExecutingChain) return;
        if (holdingOrder.Count == 0) return;

        StartCoroutine(ChainLightning());
    }

    IEnumerator ChainLightning()
    {
        isExecutingChain = true;

        // チャージが満タンの敵だけをフィルタリング（リストのコピーを作成）
        List<tekiScript> chainList = new List<tekiScript>();

        foreach (var enemy in holdingOrder)
        {
            if (enemy != null && enemy.charge != null)
            {
                // チャージが満タンかチェック
                if (enemy.charge.CurrentCharge >= enemy.charge.MaxChargeValue)
                {
                    chainList.Add(enemy);
                }
            }
        }

       // holdingOrder.Clear();

        // 満タンの敵がいない場合は終了
        if (chainList.Count == 0)
        {
            holdingOrder.Clear();
            isExecutingChain = false;
            yield break;
        }

        // 連鎖開始前に座標リストを作成（敵が途中で消えても大丈夫）
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);

        List<tekiScript> validEnemies = new List<tekiScript>();
        foreach (var enemy in chainList)
        {
            if (enemy != null)
            {
                points.Add(enemy.transform.position);
                validEnemies.Add(enemy);
            }
        }

        // 連鎖実行
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 startPos = points[i];
            Vector3 endPos = points[i + 1];

            CreateChainEffect(startPos, endPos);

            // i < validEnemies.Countで範囲チェック
            if (i < validEnemies.Count)
            {
                tekiScript targetEnemy = validEnemies[i];

                // 敵がまだ存在しているか確認
                if (targetEnemy != null && targetEnemy.charge != null)
                {
                    ShowLightningEffect(endPos);
                    yield return new WaitForSeconds(0.05f);

                    targetEnemy.charge.ForceDefeatEnemy(
                        targetEnemy.gameObject,
                        targetEnemy.AppEnemy
                    );
                }
            }

            yield return new WaitForSeconds(chainDelay);
        }

        isExecutingChain = false;
    }

    void ShowLightningEffect(Vector3 position)
    {
        if (lightningPrefab == null) return;

        GameObject lightning = Instantiate(
            lightningPrefab,
            position,
            Quaternion.identity
        );

        Destroy(lightning, hitEffectDuration);
    }

    void CreateChainEffect(Vector3 startPos, Vector3 endPos)
    {
        if (lightningPrefab == null) return;

        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;

        if (distance < 0.1f) return;

        Vector3 centerPos = startPos + direction * 0.5f;

        GameObject chainEffect = Instantiate(
            lightningPrefab,
            centerPos,
            Quaternion.identity
        );

        chainEffect.transform.forward = direction.normalized;

        if (adjustScale)
        {
            Vector3 scale = chainEffect.transform.localScale;
            scale.z = distance / baseDistance;
            chainEffect.transform.localScale = scale;
        }

        Destroy(chainEffect, chainEffectDuration);
    }
}
