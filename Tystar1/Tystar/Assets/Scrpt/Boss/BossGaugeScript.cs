using UnityEngine;
using UnityEngine.UI;

public class BossGaugeScript : MonoBehaviour
{
    [SerializeField] Image gaugeImage;
    [SerializeField] int MaxCharge = 50;

    public int currentCharge = 0;
    public KeyCode assignedKey;   // BossWaveScript から設定する
    public System.Action OnGaugeMax;  // ゲージMAX時に通知

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

        currentCharge = Mathf.Clamp(currentCharge, 0, MaxCharge);
        UpdateGauge();

        // MAXになったら BossWaveScript に知らせる
        if (currentCharge >= MaxCharge)
        {
            OnGaugeMax?.Invoke();
        }
    }

    private void UpdateGauge()
    {
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = (float)currentCharge / MaxCharge;
        }
    }
}
