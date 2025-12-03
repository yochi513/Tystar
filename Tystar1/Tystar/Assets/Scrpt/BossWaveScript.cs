using UnityEngine;

public class BossWaveScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform supar;
    [SerializeField] Transform player;

    int phase = 0;          // 3回ループ用
    GameObject ball;        // 今のボール
    char currentLetter;     // 今の文字
    KeyCode currentKey;     // 押すべきキー

    void Start()
    {
        StartNextPhase();
    }

    void StartNextPhase()
    {
        if (phase >= 3)
        {
            Debug.Log("勝利！");
            return;
        }

        phase++;

        // ランダム文字
        int n = Random.Range(0, 26);
        currentLetter = (char)('A' + n);
        currentKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentLetter.ToString());

        // ボール生成
        ball = Instantiate(ballPrefab, supar.position, Quaternion.identity);
        ball.GetComponent<BallScript>().Init(currentLetter, player, supar);
    }

    void Update()
    {
        // キー入力受付
        if (Input.GetKeyDown(currentKey))
        {
            // ボールに「反射」の命令
            ball.GetComponent<BallScript>().Reflect();

            // 次のフェーズへ
            StartNextPhase();
        }
    }
}
