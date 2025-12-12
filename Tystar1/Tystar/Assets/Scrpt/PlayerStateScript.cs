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
    public Playerscrpt playerA;

    public enum PlayerState
    {
        None,
        GinoAttack,
        BossAttackÅ@//É{ÉXçUåÇí«â¡
    }
    public static PlayerState CurrentState = PlayerState.None;

    void Start()
    {
        Arrow0.SetActive(false);
        Arrow1.SetActive(false);
        Arrow2.SetActive(true);
        Arrow3.SetActive(false);
        CurrentState = PlayerState.GinoAttack;
    }
    void Update()
    {

        if (playerA.PlayerHP <= 0)
        {
            CurrentState = PlayerState.None;
            Arrow0.SetActive(true);
            Arrow1.SetActive(false);
            Arrow2.SetActive(false);
            Arrow3.SetActive(false);

        }

    }
    public void BossAttack()
    {
        CurrentState = PlayerState.BossAttack;
    }

}