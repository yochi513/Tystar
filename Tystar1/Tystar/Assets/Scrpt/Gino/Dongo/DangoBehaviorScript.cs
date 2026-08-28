using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>団子編成内の一体。Managerから解放された順番でのみ倒せる。</summary>
public class DangoBehaviorScript : MonoBehaviour
{
    public int orderIndex;  // 0=上,1=中,2=下
    public DangoManagerScript manager;

    [HideInInspector] public bool canBeDestroyed = false;

    private tekiScript teki;

    void Start()
    {
        teki = GetComponent<tekiScript>();
        teki.enabled = false; // 最初は操作不可（上だけ許可）
    }

    void Update()
    {
        // 順番が来ていないなら反応しない
        if (!canBeDestroyed) return;

        teki.enabled = true;
    }

    public void OnDestroyed()
    {
        manager.OnDangoDestroyed(orderIndex);
    }
}
