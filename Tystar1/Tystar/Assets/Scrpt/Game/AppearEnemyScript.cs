using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>旧スポーン試作の保管用コンポーネント。現在のゲーム進行では使用しない。</summary>
public class AppearEnemyScript : MonoBehaviour
{
//    [SerializeField] GameObject[] enemyPrefabs;
//    [SerializeField] Transform[] spawnPoints; 
//    [SerializeField] int maxEnemies = 100;

//    private int totalSpawned = 0;
//    private int totalDefeated = 0;

//    private bool secondWaveSpawned = false;

//    void Start()
//    {
//        // Wave 1
//        SpawnWave();
//        // Wave 2 は数秒後
//        StartCoroutine(SpawnSecondWaveAfterDelay(2f));
//    }

//    void Update()
//    {
//        // Wave 3以降：4体倒した後
//        if (secondWaveSpawned && totalDefeated >= 4 && totalSpawned < maxEnemies)
//        {
//            totalDefeated = 0;
//            SpawnWave();
//        }
//    }

//    void SpawnWave()
//    {
//        for (int i = 0; i < spawnPoints.Length; i++)
//        {
//            if (totalSpawned >= maxEnemies) break;

//            int index = Random.Range(0, enemyPrefabs.Length);
//            GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoints[i].position, Quaternion.identity);

//            var script = enemy.GetComponent<tekiScript>();
//            if (script != null)
//            {
//               // script.AppEnemy = this;
//            }

//            totalSpawned++;
//        }
//    }

//    private IEnumerator SpawnSecondWaveAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);//指定した秒数だけ一旦処理を中断
//        SpawnWave();
//        secondWaveSpawned = true;
//    }

//    public void ReportEnemyDefeated()
//    {
//        totalDefeated++;
//    }
}
