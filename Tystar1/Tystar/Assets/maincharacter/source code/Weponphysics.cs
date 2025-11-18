using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weponphysics : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//左クリックで起動（仮置き）
        {
            rb.useGravity = true;
        }
    }
}
