using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // 雷エフェクトのPrefab（Inspectorで設定）
    public GameObject lightningPrefab;

    // 長押しされている敵の「順番」を記録するリスト
    private List<tekiScript> holdingOrder = new List<tekiScript>();

    void Update()
    {
        // 現在存在する全ての敵を取得
        tekiScript[] enemies = FindObjectsOfType<tekiScript>();

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            KeyCode key = enemy.assignedKey;

            // キーが押された瞬間 → 順番に追加
            if (Input.GetKeyDown(key))
            {
                if (!holdingOrder.Contains(enemy))
                {
                    holdingOrder.Add(enemy);
                }
            }

            // キーが離された瞬間 → 順番から削除
            if (Input.GetKeyUp(key))
            {
                if (holdingOrder.Contains(enemy))
                {
                    holdingOrder.Remove(enemy);
                }
            }
        }

        // Enterキーで連鎖雷を発動
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (holdingOrder.Count >= 1)
            {
                StartCoroutine(ChainLightning());
            }
        }
    }

    // 連鎖雷の本体（時間差で順番に処理）
    IEnumerator ChainLightning()
    {
        Debug.Log(" 連鎖雷 開始");

        // 今の順番をコピーする
        List<tekiScript> chainList = new List<tekiScript>(holdingOrder);

        for (int i = 0; i < chainList.Count; i++)
        {
            tekiScript enemy = chainList[i];

            if (enemy != null)
            {
                FireLightning(enemy);
                yield return new WaitForSeconds(0.3f);
            }
        }

        Debug.Log(" 連鎖雷 終了");
    }


    //雷の発生
    void FireLightning(tekiScript enemy)
    {
        if (lightningPrefab == null) return;

        Instantiate(
            lightningPrefab,
            enemy.transform.position,
            Quaternion.identity
        );
    }


}
