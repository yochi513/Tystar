using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>難易度別スポーナーで共通の「敵生成・文字割当・UI設定」を行う。</summary>
public static class EnemySpawnUtility
{
    public static bool TrySpawn(IList<GameObject> enemyPrefabs, Sprite[] alphabet, Transform[] spawnPoints, int spawnIndex, EnemySpponScript normalSpawner, HardEnemySpawnScript hardSpawner)
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0 || alphabet == null || alphabet.Length == 0 || spawnPoints == null || spawnIndex < 0 || spawnIndex >= spawnPoints.Length)
        {
            return false;
        }

        int letterIndex = Random.Range(0, alphabet.Length);
        GameObject enemy = Object.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPoints[spawnIndex].position, Quaternion.identity);
        tekiScript enemyScript = enemy.GetComponent<tekiScript>();

        if (enemyScript != null)
        {
            char letter = (char)('A' + letterIndex);
            enemyScript.assignedChar = letter;
            enemyScript.assignedKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), letter.ToString());
            enemyScript.AppEnemy = normalSpawner;
            enemyScript.HardEnemy = hardSpawner;
        }

        Image letterImage = enemy.GetComponentInChildren<Image>();
        if (letterImage != null)
        {
            letterImage.sprite = alphabet[letterIndex];
        }

        return true;
    }
}
