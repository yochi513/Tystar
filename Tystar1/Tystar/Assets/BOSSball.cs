using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour
{
    [SerializeField] private AudioClip throwSound; // 投擲時の効果音
    [SerializeField][Range(0f, 1f)] private float volume = 1.0f; // 音量

    private static GameObject soundPlayer; // 効果音再生用の永続オブジェクト

    void Start()
    {
        // 効果音再生用の永続オブジェクトを作成（まだなければ）
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("ProjectileSoundPlayer");
            soundPlayer.AddComponent<AudioSource>();
            DontDestroyOnLoad(soundPlayer);
        }

        // 投擲された瞬間に効果音を再生
        PlayThrowSound();
    }

    private void PlayThrowSound()
    {
        if (throwSound != null && soundPlayer != null)
        {
            AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(throwSound, volume);
            }
        }
    }
}