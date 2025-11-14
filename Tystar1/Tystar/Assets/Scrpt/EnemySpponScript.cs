using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpponScript : MonoBehaviour
{
    [SerializeField] GameObject Gino;
    [SerializeField] Sprite[] Alphabet;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int maxEnemies = 100;

    [SerializeField] float timeBetweenEnemies = 0.5f;
    [SerializeField] float timeBetweenWaves = 10f;
    [SerializeField] int stopGinoCount = 11;

    [SerializeField] int ginoMax = 10;
    [SerializeField] int totalPhaes = 3;
    public BossScript boss;

    private List<int> KillsThisFrame = new List<int>();
    private int totalSpawned = 0;
    private int totalDefeated = 0;
    private int totalGinoCount = 0;
    private int totalGinoMax = 0;
    private int totalScore = 0;
    private int currentWave = 1;
    private bool secondWaveSpawned = false;
    private bool canSpawn = true;
    private bool isSpawning = false;//ウェーブの進行がどうか
    public int score = 0;
    public UItextScript UItext;

    private List<int[]> spawnPatterns = new List<int[]>()
    {
        new int[]{0,1,2,3},
        new int[]{3,2,1,0},
        new int[]{0,2,1,3},
        new int[]{3,1,2,0},
        new int[]{0,3,1,2},
        new int[]{2,1,3,0},
    };

    void Start()
    {
        if (staticScript.ReturnedFromBoss)
        {
            // Bossから戻ってきた場合だけスポーン再開
            staticScript.ReturnedFromBoss = false; // フラグリセット

            score = staticScript.SaveScore;
            totalGinoCount = staticScript.SaveKillCount;

            UItext.SetCount(score, totalGinoCount);
            ResumeSpawn();
        }
        else
        {
            // シーン開始時は通常のスポーン
            StartCoroutine(SpawnWaveRoutine());
            score = 0;
            totalGinoCount = 0;

        }
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
    }

    private IEnumerator SpawnWaveRoutine()
    {
        if (!canSpawn) yield break;
        isSpawning = true;

        yield return StartCoroutine(SpawnEnemiesWithDelay());
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = false;


    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        //パターンランダム
        int[] chosenPattern = spawnPatterns[Random.Range(0, spawnPatterns.Count)];
        //選ばれた順番にスポーン
        foreach (int i in chosenPattern)
        {
            if (totalSpawned >= maxEnemies) yield break;
            if (i >= spawnPoints.Length) continue;

            //敵にランダムに文字付与
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
        totalGinoMax++;
        totalGinoCount++;
        totalDefeated++;
        KillsThisFrame.Add(baseScore);
       
        if (totalGinoMax >= stopGinoCount)
        {
            Debug.Log($"規定数到達: {totalGinoMax}/{stopGinoCount}");
            canSpawn = false;
            StartCoroutine(WaitAndGoBoss());
        }
    }
    private IEnumerator WaitAndGoBoss()
    {
        yield return new WaitForSeconds(0.1f);
        staticScript.LastSceneName = SceneManager.GetActiveScene().name;
        staticScript.SaveScore = totalScore;
        staticScript.SavePlayerHP = GameObject.FindWithTag("Player")
            ?.GetComponent<Playerscrpt>()?.GetCurrentHP() ?? 0;
        staticScript.SaveKillCount = totalGinoCount;

        staticScript.ReturnedFromBoss = true;
        Debug.Log("BossSceneに移動します");
        UnityEngine.SceneManagement.SceneManager.LoadScene("BossScene");
    }



    public void ResumeSpawn()
    {
        canSpawn = true;
        totalDefeated = 0;
        StartCoroutine(SpawnWaveRoutine());
    }
    public void SCORE(float Multiple, int Enemy)
    {
        int total = 0;
        foreach (int s in KillsThisFrame)
            total += s;
        total = Mathf.RoundToInt(total * Multiple);
        score += total;
        UItext.Count(score, Enemy);
        totalScore += score;
    }
}