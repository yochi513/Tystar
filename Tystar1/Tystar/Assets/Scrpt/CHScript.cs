using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHScript : MonoBehaviour
{
    [SerializeField] Image ch;
    [SerializeField] ParticleSystem beamEffect;

    private float Maxch = 50f;
    private float Minch = 0f;
    private bool isPlayingEffect = false;

    void Start()
    {
        if (beamEffect == null)
        {
            beamEffect = GetComponentInChildren<ParticleSystem>();

            if (beamEffect == null)
            {
                Debug.LogError("ParticleSystemが見つかりません！");
            }
            else
            {
                Debug.Log("子オブジェクトからbeamEffectを自動取得: " + beamEffect.name);
            }
        }
        else
        {
            Debug.Log("beamEffect設定OK: " + beamEffect.name);
        }

        if (beamEffect != null)
        {
            beamEffect.Stop();
            Debug.Log("エフェクトを停止状態に設定");
        }
    }

    void Update()
    {
        Minch = staticScript.SaveCh;
        Minch = Mathf.Clamp(Minch, 0, Maxch);
        UpdateCh();

        // ボスフェーズのみゲージ消費を有効化
        if (PlayerStateScript.CurrentState == PlayerStateScript.PlayerState.BossAttack)
        {
            Ae();
        }
        else
        {
            Debug.Log("BossAttack状態ではありません: " + PlayerStateScript.CurrentState);
        }
    }

    public void ChAdd(float point)
    {
        Minch += point;
        staticScript.SaveCh = Minch;
    }

    private void UpdateCh()
    {
        if (ch != null)
        {
            ch.fillAmount = Minch / Maxch;
        }
        //Ae();
    }

    public void Ae()
    {
        if (Input.GetKey(KeyCode.Return))
            // まず、メソッドが呼ばれているか確認
            Debug.Log("Ae()メソッドが呼ばれました");

        // エンターキーの状態を確認
        bool enterPressed = Input.GetKey(KeyCode.Return);
        Debug.Log($"エンターキー: {enterPressed}, Minch: {Minch}, isPlayingEffect: {isPlayingEffect}");

        if (enterPressed && Minch > 0)
        {
            Debug.Log("条件を満たしました！エフェクト再生処理に入ります");

            Minch -= 0.1f;

            // エフェクトを再生開始
            if (!isPlayingEffect)
            {
                Debug.Log("isPlayingEffectがfalseなので再生開始します");

                if (beamEffect != null)
                {
                    Debug.Log("beamEffect.Play() を呼び出します");
                    beamEffect.Play();

                    // エフェクトが実際に再生されているか確認
                    Debug.Log($"エフェクト再生状態: isPlaying={beamEffect.isPlaying}, isEmitting={beamEffect.isEmitting}, particleCount={beamEffect.particleCount}");

                    isPlayingEffect = true;
                    Debug.Log("ビーム発射開始！");
                }
                else
                {
                    Debug.LogError("beamEffectがnullです！");
                }
            }
            else
            {
                Debug.Log("すでに再生中です");
            }
        }
        else
        {
            if (!enterPressed)
            {
                Debug.Log("エンターキーが押されていません");
            }
            if (Minch <= 0)
            {
                Debug.Log("Minchが0以下です");
            }

            // キーを離したらエフェクトを停止
            if (isPlayingEffect)
            {
                if (beamEffect != null)
                {
                    beamEffect.Stop();
                    isPlayingEffect = false;
                    Debug.Log("ビーム停止");
                }
            }
        }

        staticScript.SaveCh = Minch;
    }
}