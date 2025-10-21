using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ChargeScript : MonoBehaviour
{

    [SerializeField] Image Char;
  private float MaxCharge = 500f;
    private float currentCharge = 0f;


    public enum Selection
    {
        Two=2,
        Three = 3,
        Four = 4,
        Five = 5

    }

    public Selection Chargetime = Selection.Three;
    public void select()
    {
        if (Chargetime == Selection.Two)
        {
            MaxCharge = 100f;
        }
        else if (Chargetime == Selection.Three)
        {
            MaxCharge = 150f;
        }
        else if (Chargetime == Selection.Four)
        {
            MaxCharge = 200f;
        }
        else if (Chargetime == Selection.Five)
        {
            MaxCharge = 250f;
        }

    }

    // ƒQ[ƒW‰ÁZi“G‚©‚çŒÄ‚Ño‚³‚ê‚éj
    public void Tystar(int amount)
    {
        select();
        if (amount == 1) 
        {
            currentCharge ++; 
        }
        else
        {
            currentCharge--;
        }
        currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
        UpdateGauge();
    }
  

    // UIXV
    private void UpdateGauge()
    {
        select();
        if (Char != null)
        {
            Char.fillAmount = currentCharge / MaxCharge;
        }
    }

    //  “G‚ğÁ‚·‚ÉoŒ»Œ³‚É’Ê’m
    public void EntarWithCallback(GameObject target, EnemySpponScript appearScript)
    {
        Debug.Log("EntarWithCallbackŒÄ‚Î‚ê‚½");
        Debug.Log("currentCharge: " + currentCharge + " / MaxCharge: " + MaxCharge);

        Debug.Log("appearScript: " + appearScript);
        select();
        if (currentCharge >= MaxCharge )
        {
            if (appearScript != null)
            {
                appearScript.ReportEnemyDefeated(); // •ñ
            }

            Destroy(target);

            currentCharge = 0f;
            UpdateGauge();
        }
      
    }


}

//300f‚Å6•b‚­‚ç‚¢
//250f‚Å5•b‚­‚ç‚¢
//200f‚Å4•b‚­‚ç‚¢
//150f‚Å3•b‚­‚ç‚¢
