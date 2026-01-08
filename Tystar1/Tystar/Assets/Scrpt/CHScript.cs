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
        }

        if (beamEffect != null)
        {
            beamEffect.Stop();
        }
    }

    void Update()
    {
        Minch = staticScript.SaveCh;
        Minch = Mathf.Clamp(Minch, 0, Maxch);
        UpdateCh();

        // ボスフェーズのみゲージ消費とエフェクトを有効化
        if (PlayerStateScript.CurrentState == PlayerStateScript.PlayerState.BossAttack)
        {
            Ae();
        }
        else
        {
            // ボスフェーズ以外ではエフェクトを停止
            if (isPlayingEffect && beamEffect != null)
            {
                beamEffect.Stop();
                isPlayingEffect = false;
            }
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
    }

    public void Ae()
    {
        // 雷エフェクト実行中は何もしない
        if (Lightning.isExecutingChain)
        {
            if (isPlayingEffect && beamEffect != null)
            {
                beamEffect.Stop();
                isPlayingEffect = false;
            }
            return;
        }

        bool enterPressed = Input.GetKey(KeyCode.Return);

        if (enterPressed && Minch > 0)
        {
            // チャージを消費（毎フレーム）
            Minch -= 10f * Time.deltaTime; // 1秒で10消費
            Minch = Mathf.Max(Minch, 0);

            // エフェクトを再生
            if (!isPlayingEffect && beamEffect != null)
            {
                beamEffect.Play();
                isPlayingEffect = true;
                Debug.Log("ビーム発射開始！");
            }
        }
        else
        {
            // キーを離したらエフェクトを停止
            if (isPlayingEffect && beamEffect != null)
            {
                beamEffect.Stop();
                isPlayingEffect = false;
                Debug.Log("ビーム停止");
            }
        }

        // 静的変数に反映
        staticScript.SaveCh = Minch;
    }
}