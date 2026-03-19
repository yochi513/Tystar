using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private int EnemyCount = 0;
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
        EnemyCount =0;
    }

    // 同時に撃破した時の処理
    void Update()
    {
        if (KillThisFrame.Count > 0)
        {
            if (KillThisFrame.Count >= 4)
                Score(+4);
            else if (KillThisFrame.Count == 3)
                Score(+3);
            else if (KillThisFrame.Count == 2)
                Score(+2);
            else 
                Score(+1);
        }
        KillThisFrame.Clear();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            int patternIndex = Random.Range(0, SpawnPattern.Count);
            int[] currentPattern = SpawnPattern[patternIndex];

            foreach (int pointIndex in currentPattern)
            {
                SpawnEnemy(pointIndex);

                // パターン内で少しズラして出したい場合はここを有効にする
                yield return new WaitForSeconds(TimeBetEnemy);
            }

          //  yield return new WaitForSeconds(TimeBetEnemy);

        }
    }
    private void SpawnEnemy(int pointIndex)
    {
        if (ENEMYLIST.Count == 0 || SpawnPoint.Length == 0) return;

        int index = Random.Range(0,Alphabet.Length);
        char letter = (char)('A'+index);

        GameObject EnemyPrefab = ENEMYLIST[Random.Range(0, ENEMYLIST.Count)];
        GameObject enemy = Instantiate(EnemyPrefab, SpawnPoint[pointIndex].position, Quaternion.identity);

        var script = enemy.GetComponent<tekiScript>();
        if (script != null)
        {
            script.assignedChar= letter;
            script.assignedKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), letter.ToString());
        }

        Image img = enemy.GetComponentInChildren<Image>();
        if (img != null)
        {
            img.sprite = Alphabet[index];
        }
    }
        public void ReportEnemy()
    {
        EnemyCount++;
    }
    public void Score(int a)
    {
        EneUI.Count(a);
    }
}
