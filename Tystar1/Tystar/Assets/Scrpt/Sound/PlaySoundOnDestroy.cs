using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{
    [SerializeField] private AudioClip defeatSound; // 効果音
    [SerializeField] private float volume = 1.0f; // 音量

    private static GameObject soundPlayer; // 効果音再生用の永続オブジェクト

    void Start()
    {
        // 効果音再生用の永続オブジェクトを作成（まだなければ）
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            soundPlayer.AddComponent<AudioSource>();
            DontDestroyOnLoad(soundPlayer); // シーン切り替えでも消えない
        }
    }

    void OnDestroy()
    {
        // このオブジェクトが破壊される時に効果音を再生
        if (defeatSound != null && soundPlayer != null)
        {
            AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(defeatSound, volume);
            }
        }
    }
}
