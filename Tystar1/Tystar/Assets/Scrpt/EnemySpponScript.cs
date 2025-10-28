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

    [SerializeField] float timeBetweenEnemies = 0.5f;
    [SerializeField] float timeBetweenWaves = 10f;

    [SerializeField] int ginoMax = 10;
    [SerializeField] int totalPhaes = 3;
    public BossScript boss;

    private int totalSpawned = 0;
    private int totalDefeated = 0;
    private int currentWave = 1;
    private bool secondWaveSpawned = false;
    private bool isSpawning = false;//ウェーブの進行がどうか
    public UItextScript UItext;

    void Start()
    {
        StartCoroutine(SpawnWaveRoutine());
        //boss.EnableAttack(false);
    }

    void Update()
    {
        if (!isSpawning && totalDefeated >= spawnPoints.Length && totalSpawned < maxEnemies)
        {
            totalDefeated = 0;
            currentWave++;
            StartCoroutine(SpawnWaveRoutine());
        }
        //if (!isSpawning && totalDefeated >= ginoMax)
        //{
        //  totalDefeated = 0;
        // // boss.EnableAttack(true);
        //}


        //if(boss.IsAttacked&&currentWave<totalPhaes)
        //{
        //   // boss.EnableAttack(false);
        //    currentWave++;
        //    StartCoroutine(SpawnWaveRoutine());
        //}
    }

    private IEnumerator SpawnWaveRoutine()
    {
        isSpawning = true;
        yield return StartCoroutine(SpawnEnemiesWithDelay());
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = false;

      
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

            Image img = enemy.GetComponentInChildren<Image>();
            if (img != null && index < Alphabet.Length)
            {
                img.sprite = Alphabet[index];
            }

            totalSpawned++;
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    public void ReportEnemyDefeated()
    {
        totalDefeated++;
        UItext.Count(100, 1);

    }
}
