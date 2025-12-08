using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    char letter;
    bool reflected = false;
    [SerializeField] Image letterImage;   // ← ボールの子のUI画像
    public int Num = 15;
    private int Count=0;
    Transform player;
    Transform supar;

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
