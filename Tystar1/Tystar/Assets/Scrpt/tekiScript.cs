using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tekiScript : MonoBehaviour
{
    private GameObject target;
   private float speed = 0.05f;

    public string word;
    public KeyCode assignedKey;
    public char assignedChar;
    public Color enemyColor;


    public ChargeScript charge;
    public AppearEnemyScript AppEnemy;

    private Renderer rend;
    private TextMeshPro textDisplay;

    public enum SPEED
    {
        One=1,
        Two = 2,
        Three = 3,
        Four = 4,
    }


    public SPEED speedtime =SPEED.One;
    public void Speed()
    {
        if (speedtime == SPEED.One)
        {
            speed = 0.05f;
        }
        else if (speedtime == SPEED.Two)
        {
            speed = 0.1f;
        }
        else if (speedtime == SPEED.Three)
        {
            speed = 0.15f;
        }
        else if(speedtime == SPEED.Four)
        {
            speed = 0.2f;
        }
    }
    public void State()
    {
        switch (PlayerStateScript.CurrentState)
        {
           case PlayerStateScript.PlayerState.GinoAttack:
                GinoAttack();
                break;

           case PlayerStateScript.PlayerState.BossAttack:
                BossAttack(); 
                break;

           case PlayerStateScript.PlayerState.Defense: 
                Defense();
                break;
        }
    }

    void Start()
    {
        Speed();
        target = GameObject.Find("player");
        rend = GetComponent<Renderer>();
        rend.material.color = enemyColor;

        textDisplay = GetComponentInChildren<TextMeshPro>();
        if (textDisplay != null)
        {
            textDisplay.text = assignedKey.ToString();
        }
        charge.Tystar(0);
    }

    void Update()
    {
        State();
        transform.LookAt(target.transform);
        transform.position += transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーからPlayerHealthを取得
            Playerscrpt playerHealth = other.GetComponent<Playerscrpt>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // 敵のダメージ量
            }
        }
    }

    void GinoAttack()
    {
        Debug.Log("ギノキー反応");
        if (Input.GetKey(assignedKey))//キー入力を受付してる
        {
            charge.Tystar(+1);
            charge.EntarWithCallback(gameObject, AppEnemy);
           
        }
        else
        {
            charge.Tystar(-1);
          
        }
    }
    void BossAttack()
    {
        Debug.Log("ボスキー反応");
    }
    void Defense()
    {
        Debug.Log("防御キー反応");
    }
}
