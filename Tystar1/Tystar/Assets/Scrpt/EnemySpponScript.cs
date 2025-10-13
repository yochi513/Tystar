using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpponScript : MonoBehaviour
{
    [SerializeField] GameObject Gino;
    [SerializeField] Sprite[] Alphabet;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int maxEnemies = 100;

   
    [SerializeField] float timeBetweenEnemies = 0.5f;  // 1‘Ì‚¸‚Âo‚·ŠÔŠu
    [SerializeField] float timeBetweenWaves = 10f;      // Wave“¯m‚ÌŠÔŠu

    private int totalSpawned = 0;
    private int totalDefeated = 0;
    private bool secondWaveSpawned = false;

    void Start()
    {
        // Wave1‚ğŠJn
        StartCoroutine(SpawnWaveRoutine());
    }

    void Update()
    {
        // Wave3ˆÈ~F4‘Ì“|‚µ‚½‚çÄ‚ÑSpawnWave‚ğŒÄ‚Ô
        if (secondWaveSpawned && totalDefeated >= 4 && totalSpawned < maxEnemies)
        {
            totalDefeated = 0;
            StartCoroutine(SpawnWaveRoutine());
        }
    }

    private IEnumerator SpawnWaveRoutine()
    {
        // 1ƒEƒF[ƒu“à‚Ì“G‚ğ‡”Ô‚Éo‚·
        yield return StartCoroutine(SpawnEnemiesWithDelay());

        // Ÿ‚ÌWave‚Ü‚Å‘Ò‹@
        yield return new WaitForSeconds(timeBetweenWaves);

        if (!secondWaveSpawned)
        {
            secondWaveSpawned = true;
            yield return StartCoroutine(SpawnEnemiesWithDelay());
        }
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (totalSpawned >= maxEnemies) yield break;

            int index = Random.Range(0, Alphabet.Length);
            char letter = (char)('A' + index);

            GameObject enemy = Instantiate(Gino, spawnPoints[i].position, Quaternion.identity);

            var script = enemy.GetComponent<tekiScript>();
            if (script != null)
            {
                script.AppEnemy = this;
                script.assignedChar = letter;
                script.assignedKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), letter.ToString());
            }

            // Canvasã‚ÌImage‚ÉƒXƒvƒ‰ƒCƒg‚ğ”½‰f
            Image img = enemy.GetComponentInChildren<Image>();
            if (img != null && index < Alphabet.Length)
            {
                img.sprite = Alphabet[index];
            }

            totalSpawned++;

            // “G‚ÌoŒ»‚ğ­‚µ’x‚ç‚¹‚é
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    public void ReportEnemyDefeated()
    {
        totalDefeated++;
    }
}
