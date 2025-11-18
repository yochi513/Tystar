using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    private bool canAttack = false;
    public bool IsAttacked { get; private set; } = false;

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
       // bossManager();
    }
    void bossManager()
    {
        HP--;
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
    public void EnableAttack(bool enable)
    {
        canAttack = enable;
        IsAttacked = false;
       
    }
    void OnMouseDown()
    {
        if (canAttack)
        {
          
            IsAttacked = true;
        }
    }
}
