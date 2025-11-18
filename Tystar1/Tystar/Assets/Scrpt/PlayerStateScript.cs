using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStateScript : MonoBehaviour
{
    [SerializeField] GameObject Arrow0;
    [SerializeField] GameObject Arrow1;
    [SerializeField] GameObject Arrow2;
    [SerializeField] GameObject Arrow3;

    public enum PlayerState
    {
        None,
        GinoAttack,
        BossAttack,
        Defense
    }
    public static PlayerState CurrentState = PlayerState.None;

  void Start()
    {
        Arrow0.SetActive(true);
        Arrow1.SetActive(false);
        Arrow2.SetActive(false);
        Arrow3.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentState = PlayerState.Defense;
            Arrow0.SetActive(false);
            Arrow1.SetActive(false);
            Arrow2.SetActive(false);
            Arrow3.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentState = PlayerState.GinoAttack;
            Arrow0.SetActive(false);
            Arrow1.SetActive(false);
            Arrow2.SetActive(true);
            Arrow3.SetActive(false);
   
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentState = PlayerState.BossAttack;
            Arrow0.SetActive(false);
            Arrow1.SetActive(true);
            Arrow2.SetActive(false);
            Arrow3.SetActive(false);
        }
    }

   

}
