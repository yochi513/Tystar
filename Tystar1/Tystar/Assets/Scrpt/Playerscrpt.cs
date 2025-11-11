using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscrpt : MonoBehaviour
{
   
    public int PlayerHP = 3;
    public bool isInvincible = false;
    public float invincibleTime = 1.5f;

    [SerializeField] Canvas Gameover;
    [SerializeField] Canvas MainCanvas;

     void Start()
    {
        Gameover.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
  

    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        PlayerHP -= damage;

        StartCoroutine(InvincibleCoroutine());
        if (PlayerHP <= 0)
        {
            GameOVER();
        }
    }
    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true; // –³“GON
        yield return new WaitForSeconds(invincibleTime); // Œˆ‚ß‚½ŽžŠÔ‘Ò‚Â
        isInvincible = false; // –³“GOFF
    }
    public void GameOVER()
    {
            //Debug.Log("ƒvƒŒƒCƒ„[‚ª“|‚ê‚½");
            Gameover.gameObject.SetActive(true);
            MainCanvas.gameObject.SetActive(false);
    }

}
