using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangoBehaviorScript : MonoBehaviour
{
    public int orderIndex;  // 0=ã,1=’†,2=‰º
    public DangoManagerScript manager;

    [HideInInspector] public bool canBeDestroyed = false;

    private tekiScript teki;

    void Start()
    {
        teki = GetComponent<tekiScript>();
        teki.enabled = false; // Å‰‚Í‘€ì•s‰Âiã‚¾‚¯‹–‰Âj
    }

    void Update()
    {
        // ‡”Ô‚ª—ˆ‚Ä‚¢‚È‚¢‚È‚ç”½‰‚µ‚È‚¢
        if (!canBeDestroyed) return;

        teki.enabled = true;
    }

    public void OnDestroyed()
    {
        manager.OnDangoDestroyed(orderIndex);
    }
}