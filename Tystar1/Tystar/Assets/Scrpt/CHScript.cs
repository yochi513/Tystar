using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHScript : MonoBehaviour
{
    [SerializeField] Image ch;
    private float Maxch = 50f;
    private float Minch = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Minch = Mathf.Clamp(Minch, 0, Maxch);
        UpdateCh();
    }
    public void ChAdd(float point)
    {
        Minch += point;
    }
    private void UpdateCh()
    {
        if (ch != null)
        {
            ch.fillAmount = Minch / Maxch;
        }
        // Ae();
    }

    //chゲージエンター長押しで減る
    public void Ae()
    {
        if (Input.GetKey(KeyCode.Return))
            Minch -= 0.1f;
    }

}