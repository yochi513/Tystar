using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>クリック入力に合わせて設定済みの効果音を再生する。</summary>
public class AudioConfig : MonoBehaviour
{
    [SerializeField] AudioSource seAudioSourse;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            seAudioSourse.Play();
        }
    }
}
