using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    
    public float HP = 0;

   public enum Hp
    {
        Easy=1,
       Normal=2,
        Hard=3
    }
    public static Hp Statehp = Hp.Normal;
    // Start is called before the first frame update
    void HPP()
    {
        if (Statehp == Hp.Easy)
        {
            HP = 50;
        }
        else if (Statehp == Hp.Normal)
        {
            HP = 100;
        }
        else if (Statehp == Hp.Hard)
        {
            HP = 200;
        }
    }
    void Start()
    {
        HPP();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP==0)
        {
            Destroy(gameObject);
        }
    }
}
