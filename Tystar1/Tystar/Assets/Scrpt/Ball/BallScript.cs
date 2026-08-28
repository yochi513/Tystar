using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static BallScript;

/// <summary>ボスとプレイヤーの間を移動するボール。反射後はボスへ戻る。</summary>
public class BallScript : MonoBehaviour
{
    bool reflected = false;
    [SerializeField] Image letterImage;   // ← ボールの子のUI画像
    private int Num = 5;
    Transform player;
    Transform supar;

    public enum BallTime
    {
        easy,
        normal,
        hard,
    }

    public BallTime time = BallTime.easy;

    void balltime()
    {
        switch (time)
        {
            case BallTime.easy: Num = 5; break;
            case BallTime.normal: Num = 18; break;
            case BallTime.hard: Num = 27; break;
        }
    }
    void Start()
    {
        balltime();
    }
    public void Init(char letter, KeyCode key, Transform player, Transform supar, Sprite sprite)
    {
        this.player = player;
        this.supar = supar;
        // Sprite 反映！
        if (letterImage != null)
        {
            letterImage.sprite = sprite;
        }
    }
    void Update()
    {
        Transform target = reflected ? supar : player;
        if (target == null) return;

        float speed = reflected ? 100f : Num;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.SqrMagnitude(transform.position - target.position) < 0.0001f)
        {
            Destroy(gameObject);
        }
    }
    public void Reflect()
    {
      //  Debug.Log("Reflect呼ばれてる");
        if (reflected) return;
        reflected = true;
    }
}
