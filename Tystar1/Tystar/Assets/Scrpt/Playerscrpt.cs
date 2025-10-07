using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscrpt : MonoBehaviour
{
    // 移動速度と回転速度を設定
   // public float moveSpeed = 5f;
    //public float rotationSpeed = 100f;
    public int PlayerHP = 1;
    [SerializeField] Canvas Gameover;
     void Start()
    {
        Gameover.gameObject.SetActive(false);    
    }

    void Update()
    {
  

    }
    public void TakeDamage(int damage)
    {
        PlayerHP -= damage;

        if (PlayerHP <= 0)
        {
            Debug.Log("プレイヤーが倒れた");
            Gameover.gameObject.SetActive(true);
            // ゲームオーバー処理など
        }
    }
}
//     // 上下キーで前後に移動
//if (Input.GetKey(KeyCode.UpArrow)|| (Input.GetKey(KeyCode.DownArrow)))
//     {
//         float move = Input.GetAxis("Vertical")* moveSpeed * Time.deltaTime;
//         transform.Translate(0, 0, move);

//     }
//     // 左右キーで横移動
//     if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.LeftArrow)))
//     {
//         float move2 = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
//         transform.Translate(move2, 0, 0);

//     }