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
