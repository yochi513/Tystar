using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Hardモードの撃破数表示を、値が変わった時だけ更新する。</summary>
public class EnemyUIScript : MonoBehaviour
{
    public Text EneCountText;
    private int count=0;
    public void Count(int x)
    {
        count +=x;
        CountText();
    }
    public void CountText()
    {
        if (EneCountText != null)
        {
            EneCountText.text = $"ギノ討伐数:{count}";
        }
    }
   
   
}
