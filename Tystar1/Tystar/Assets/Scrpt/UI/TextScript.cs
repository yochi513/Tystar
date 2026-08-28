using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>ワールド空間の文字を、常にカメラ正面へ向ける。</summary>
public class TextScript : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (cameraTransform == null) return;
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
