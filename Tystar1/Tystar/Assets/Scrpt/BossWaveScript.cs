using UnityEngine;

public class BossWaveScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform supar;
    [SerializeField] Transform player;
    [SerializeField] Sprite[] Alphabet;

    public BallCHScrpt BallCH;
    public PlayerStateScript playerState;
    public BallScript Ball;

    int phase = 0;
    GameObject ball;
    char currentLetter;
    public KeyCode currentKey;
    Sprite currentSprite;

    private bool isWaitingForReflect = false;
    private bool bossPhaseStarted = false; // ボスフェーズ開始フラグ

    void Start()
    {
        StartNextPhase();
       
    }

    void StartNextPhase()
    {
        if (phase >= 3)
        {
            staticScript.BossCount++;
            Debug.Log("Bossフェーズ突入回数" + staticScript.BossCount);
            if (!bossPhaseStarted)
            {
                bossPhaseStarted = true;
                playerState.BossAttack();
                Debug.Log("ボス攻撃フェーズ開始！ステート: " + PlayerStateScript.CurrentState);
                Debug.Log("現在のチャージ量: " + staticScript.SaveCh);
            }
            return;
        }

        int index = Random.Range(0, Alphabet.Length);
        currentLetter = (char)('A' + index);
        currentKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentLetter.ToString());
        currentSprite = Alphabet[index];

        ball = Instantiate(ballPrefab, supar.position, Quaternion.identity);
        ball.GetComponent<BallScript>().Init(currentLetter, currentKey, player, supar, currentSprite);

        isWaitingForReflect = true;
        Debug.Log($"フェーズ{phase + 1}: キー '{currentLetter}' を押してボールを跳ね返してください");
    }

    void Update()
    {
        // ボスフェーズに移行したらキー入力を受け付けない
        if (bossPhaseStarted) return;

        if (isWaitingForReflect && Input.GetKeyDown(currentKey))
        {
            ball.GetComponent<BallScript>().Reflect();

            Animator suparAnimator = supar.GetComponent<Animator>();
            if (suparAnimator != null)
            {
                suparAnimator.SetTrigger("Attack");
            }

            isWaitingForReflect = false;
            phase++;

            Invoke(nameof(StartNextPhase), 1f);
        }
    }
}