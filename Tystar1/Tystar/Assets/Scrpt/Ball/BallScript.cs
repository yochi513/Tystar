using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static BallScript;

public class BallScript : MonoBehaviour
{
    char letter;
    bool reflected = false;
    [SerializeField] Image letterImage;   // ← ボールの子のUI画像
    private int Num = 5;
    private int Count=0;
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
        if (player == null) return;
 
    if (player == supar)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 100f * Time.deltaTime);
        }
    //if (Count==0)
    //    {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Num * Time.deltaTime);
        //}

    }
    public void Reflect()
    {
      //  Debug.Log("Reflect呼ばれてる");
        if (reflected) return;
        reflected = true;
        player = supar;
      
        
    }
}
