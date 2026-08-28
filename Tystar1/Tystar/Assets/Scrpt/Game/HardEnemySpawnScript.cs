using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Hard用の敵ウェーブを生成し、指定した総数で生成を停止する。</summary>
public class HardEnemySpawnScript : MonoBehaviour
{
    [Header("スポーンする敵の設定")]
    [SerializeField] List<GameObject> ENEMYLIST = new List<GameObject>();
    [SerializeField] Sprite[] Alphabet;
    [SerializeField] Transform[] SpawnPoint;
    [SerializeField] int maxEnemyNum = 1000;

    [Header("スポーン間隔と次のウェーブ間隔")]
    [SerializeField] float TimeBetEnemy = 0.5f;
    [SerializeField] float TimeBetWave = 10f;

    private List<int> KillThisFrame = new List<int>();
    private int spawnedCount;
    private bool canSpawn = true;
    public EnemyUIScript EneUI;


    //スポーンパターン
    private List<int[]> SpawnPattern = new List<int[]>()
    {
        new int[] {0,1,2,3,4,5},
        new int[] {0,3,2,5,4,1},
        new int[] {5,3,1,0,2,4},
        new int[] {1,4,5,3,0,2},
        new int[] {2,1,0,3,4,5},
        new int[] {3,4,2,5,1,0},

    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
        spawnedCount = 0;
    }

    // 同時に撃破した時の処理
    void Update()
    {
        if (KillThisFrame.Count > 0 && EneUI != null)
        {
            EneUI.Count(KillThisFrame.Count);
        }
        KillThisFrame.Clear();
    }

    private IEnumerator SpawnLoop()
    {
        while (canSpawn && spawnedCount < maxEnemyNum)
        {
            int patternIndex = Random.Range(0, SpawnPattern.Count);
            int[] currentPattern = SpawnPattern[patternIndex];

            foreach (int pointIndex in currentPattern)
            {
                if (!canSpawn || spawnedCount >= maxEnemyNum) yield break;
                SpawnEnemy(pointIndex);

                // パターン内で少しズラして出したい場合はここを有効にする
                yield return new WaitForSeconds(TimeBetEnemy);
            }

          //  yield return new WaitForSeconds(TimeBetEnemy);

        }
    }
    private void SpawnEnemy(int pointIndex)
    {
        if (EnemySpawnUtility.TrySpawn(ENEMYLIST, Alphabet, SpawnPoint, pointIndex, null, this))
        {
            spawnedCount++;
        }
    }
    public void ReportEnemyDefeated()
    {
        // 敵側から届く撃破通知を、同フレーム集計用の一覧へ追加する。
        KillThisFrame.Add(1);
    }
   
}
