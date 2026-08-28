using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
