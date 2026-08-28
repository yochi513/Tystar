using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>団子編成の撃破順を管理し、次に倒せる敵を解放する。</summary>
public class DangoManagerScript : MonoBehaviour
{
    public DangoBehaviorScript[] dangoList; // 上→中→下 の順にセット
    private int currentIndex = 0;

    void Start()
    {
        // 最初は上だけが破壊可能
        dangoList[0].canBeDestroyed = true;
    }

    public void OnDangoDestroyed(int destroyedIndex)
    {
        // 正しい順番だった時のみ次を解放
        if (destroyedIndex == currentIndex)
        {
            currentIndex++;
            if (currentIndex < dangoList.Length)
            {
                dangoList[currentIndex].canBeDestroyed = true;
            }
        }
    }
}
