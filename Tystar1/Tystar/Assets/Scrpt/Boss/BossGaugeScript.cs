using UnityEngine;
using UnityEngine.UI;

/// <summary>指定キーの長押しで増減するボス用ゲージを管理し、満タンを一度だけ通知する。</summary>
public class BossGaugeScript : MonoBehaviour
{
    [SerializeField] Image gaugeImage;
    [SerializeField] int MaxCharge = 50;

    public int currentCharge = 0;
    public KeyCode assignedKey;   // BossWaveScript から設定する
    public System.Action OnGaugeMax;  // ゲージMAX時に通知
    private bool hasNotifiedMax;

    void Update()
    {
        if (Input.GetKey(assignedKey))
        {
            Tystar(+1);
        }
        else
        {
            Tystar(-1);
        }
    }

    public void Tystar(int amount)
    {
        if (amount == 1)
            currentCharge++;
        else
            currentCharge--;

        if (MaxCharge <= 0) return;

        currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
        UpdateGauge();

        // MAXになったら BossWaveScript に知らせる
        if (currentCharge >= MaxCharge && !hasNotifiedMax)
        {
            hasNotifiedMax = true;
            OnGaugeMax?.Invoke();
        }
        else if (currentCharge < MaxCharge)
        {
            hasNotifiedMax = false;
        }
    }

    private void UpdateGauge()
    {
        if (gaugeImage != null && MaxCharge > 0)
        {
            gaugeImage.fillAmount = (float)currentCharge / MaxCharge;
        }
    }
}
