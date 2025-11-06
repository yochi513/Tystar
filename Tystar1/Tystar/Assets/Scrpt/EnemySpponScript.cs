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

    private List<int> KillsThisFrame=new List<int>();
    private int totalSpawned = 0;
    private int totalDefeated = 0;
    private int currentWave = 1;
    private bool secondWaveSpawned = false;
    private bool isSpawning = false;//ウェーブの進行がどうか
    public int score=0;
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
        // 1フレーム中に複数撃破があったら同時撃破扱い
        if (KillsThisFrame.Count > 0)
        {
            if (KillsThisFrame.Count >= 4)
                SCORE(1.5f, 4);
            else if (KillsThisFrame.Count == 3)
                SCORE(1.25f, 3);
            else if (KillsThisFrame.Count == 2)
                SCORE(1.1f, 2);
            else
                SCORE(1f, 1);
        }
        //フレーム終了時にリセット
        KillsThisFrame.Clear();
        
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
    //敵が死んだら呼ばれる
    public void ReportEnemyDefeated(int baseScore)
    {
        totalDefeated++;
        KillsThisFrame.Add(baseScore);
       
    }
    public void SCORE(float Multiple ,int Enemy)
    {
        int total = 0;
        foreach(int s in KillsThisFrame)
            total += s;
        total=Mathf.RoundToInt(total*Multiple);
        score += total;
        UItext.Count(score, Enemy);
    }
}
