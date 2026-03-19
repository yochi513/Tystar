using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIScript : MonoBehaviour
{
    public Text EneCountText;
    private int count;
    // Update is called once per frame
    private void Start()
    {
        count = 0;        
    }
    void Update()
    {
        CountText();
       // Debug.Log(count);
    }
    public void Count(int x)
    {
        count +=x;
        CountText();
    }
    public void CountText()
    {
        EneCountText.text = $"ÉMÉmì¢î∞êî:{count}";
    }
   
   
}
