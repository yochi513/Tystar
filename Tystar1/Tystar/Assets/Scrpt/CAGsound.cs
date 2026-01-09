using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyChargingSound : MonoBehaviour
{
    [SerializeField] private AudioClip chargingSound;
    [SerializeField][Range(0f, 1f)] private float volume = 1.0f;

    private AudioSource audioSource;
    private bool isCharging = false;

    // Start メソッドは1つだけ
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Return) && !isCharging)
        {
            isCharging = true;
            if (chargingSound != null)
            {
                audioSource.clip = chargingSound;
                audioSource.Play();
                Debug.Log("効果音再生開始");
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isCharging = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("効果音停止");
            }
        }
    }

    public void StopSound()
    {
        isCharging = false;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
