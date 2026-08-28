using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>主人公のチャージゲージ、ボス中のビーム、チャージ切れ時の終了処理を管理する。</summary>
public class CHScript : MonoBehaviour
{
    [SerializeField] Image ch;
    [SerializeField] ParticleSystem beamEffect;
    [SerializeField] Transform beamOrigin;

    private float Maxch = 50f;
    private float Minch = 0f;
    private bool isPlayingEffect = false;
    private bool chargeWasBeingUsed = false;
    public BossScript bossScript;

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
            // Prefabアセットが設定されている場合は、主人公の子として実体を生成する。
            if (!beamEffect.gameObject.scene.IsValid())
            {
                if (beamOrigin == null)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    beamOrigin = player != null ? player.transform : transform;
                }

                beamEffect = Instantiate(beamEffect, beamOrigin);
                beamEffect.transform.localPosition = Vector3.zero;
                beamEffect.transform.localRotation = Quaternion.identity;
            }

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
        Minch = Mathf.Clamp(Minch + point, 0, Maxch);
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
        // ボス中だけ呼ばれる。Enter長押しでゲージを消費しながらビームを維持する。
        bool enterPressed = Input.GetKey(KeyCode.Return);

        if (enterPressed && Minch > 0)
        {
            // チャージを消費（毎フレーム）
            Minch -= 10f * Time.deltaTime; // 1秒で10消費
            Minch = Mathf.Max(Minch, 0);
            chargeWasBeingUsed = true;

            // エフェクトを再生
            if (!isPlayingEffect && beamEffect != null)
            {
                if (beamEffect != null)
                {
                    beamEffect.Play();
                    isPlayingEffect = true;
                }
            }
        }
        else
        {
            // キーを離したらエフェクトを停止
            if (isPlayingEffect && beamEffect != null)
            {
                beamEffect.Stop();
                isPlayingEffect = false;
            }
        }

        // チャージを使い切った時だけ終了する。開始時に0でも即終了しない。
        if (chargeWasBeingUsed && Minch <= 0)
        {
            chargeWasBeingUsed = false;
            bossScript?.BossTime();
        }

        // 静的変数に反映
        staticScript.SaveCh = Minch;
    }
}
