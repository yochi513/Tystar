using UnityEngine;

public class BossWaveScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform supar;
    [SerializeField] Transform player;
    [SerializeField] Sprite[] Alphabet;

    public BallCHScrpt BallCH;
    int phase = 0;          // 3回ループ用
    GameObject ball;        // 今のボール
    char currentLetter;     // 今の文字
  public  KeyCode currentKey;     // 押すべきキー
    Sprite currentSprite;
    public BallScript Ball;

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

        
        int index = Random.Range(0, Alphabet.Length);

        currentLetter = (char)('A' + index);
        currentKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentLetter.ToString());
        currentSprite = Alphabet[index];
        // 対応スプライトを選ぶ
        // ボール生成
        ball = Instantiate(ballPrefab, supar.position, Quaternion.identity);

        // ボールへ渡す
        ball.GetComponent<BallScript>().Init( currentLetter,currentKey,player, supar,currentSprite);
        phase += 1;
    }

    void Update()
    {
        Debug.Log("回数"+phase);
        if (Input.GetKeyDown(currentKey))
        {
            ball.GetComponent<BallScript>().Reflect();
            StartNextPhase();
           
        }
        //if (phase == 3)
        //{
        //    Destroy(ballPrefab);
        //}
        // Debug.Log("Update動いてる");
        //if (Input.GetKeyDown(currentKey))
        //{
        //    Debug.Log("キー入力検出！ " + currentKey);
        //    //  Debug.Log("呼ばれている: BallCH = " + BallCH);
        //    Ball.Reflect();
        //    BallCH.BallCharge(+1);
        //    BallCH.EntarWithCallBack();
        //}
        //else
        //{
        //    BallCH.BallCharge(-1);
        //}
    }
}
