using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    char letter;
    bool reflected = false;
    [SerializeField] Image letterImage;   // ← ボールの子のUI画像

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
        transform.position = Vector3.MoveTowards(transform.position,player.position,5f * Time.deltaTime);
    if (player == supar)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 100f * Time.deltaTime);
        }
    }
    public void Reflect()
    {
        if (reflected) return;
        reflected = true;
        player = supar;
    }
}
