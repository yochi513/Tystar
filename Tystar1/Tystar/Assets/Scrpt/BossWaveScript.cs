using UnityEngine;

public class BossWaveScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform supar;
    [SerializeField] Transform player;
    [SerializeField] Sprite[] Alphabet;


    int phase = 0;          // 3回ループ用
    GameObject ball;        // 今のボール
    char currentLetter;     // 今の文字
    KeyCode currentKey;     // 押すべきキー
    Sprite currentSprite;

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
        int index = Random.Range(0, Alphabet.Length);

        currentLetter = (char)('A' + index);
        currentKey = KeyCode.A + index;  // ← Parse をやめて安全にする
        currentSprite = Alphabet[index];
        // 対応スプライトを選ぶ
        // ボール生成
        ball = Instantiate(ballPrefab, supar.position, Quaternion.identity);

        // ボールへ渡す
        ball.GetComponent<BallScript>().Init(
            currentLetter,
            currentKey,
            player,
            supar,
            currentSprite
        );
    }

    void Update()
    {
        if (Input.GetKeyDown(currentKey))
        {
            ball.GetComponent<BallScript>().Reflect();
            StartNextPhase();
        }
    }
}
