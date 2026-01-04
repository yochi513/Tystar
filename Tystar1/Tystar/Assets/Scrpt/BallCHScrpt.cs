using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCHScrpt : MonoBehaviour
{
    [SerializeField] Image Char;
    private float MaxCharge = 50f;
    private float currentCharge = 0f;
    public BallScript Ball;

    public void BallCharge(int point)
    {
     if (point == 1) {currentCharge++; }
     else {currentCharge--;}
     currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
     UpGame();
    }
    private void UpGame()
    {if(Char != null) { Char.fillAmount = currentCharge/MaxCharge; }}

    public void EntarWithCallBack()
    {
        Debug.Log("コールバックよばれたよーん");
       //if (currentCharge >= MaxCharge && Input.GetKeyDown(KeyCode.Return)) {
           Ball.Reflect();
            //Destroy(target);
          currentCharge = 0f;
            UpGame();
       // }
       
    }
}
