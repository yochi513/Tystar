using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextScript : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
