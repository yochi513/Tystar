using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIScript : MonoBehaviour
{
    public Text EneCountText;
    private int count=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountText();
      
    }
    public void Count(int x)
    {
        count += x;
    }
    public void CountText()
    {
        EneCountText.text = $"ÉMÉmì¢î∞êî:{count}";
    }
   
   
}
