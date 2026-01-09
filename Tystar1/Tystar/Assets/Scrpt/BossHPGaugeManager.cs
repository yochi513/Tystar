using UnityEngine;
using UnityEngine.UI;

public class BossHPGaugeManager : MonoBehaviour
{
    [SerializeField] private Image bossHPGaugeImage;

    void Start()
    {
        // UIのImageを取得（インスペクターで設定されていない場合）
        if (bossHPGaugeImage == null)
        {
            bossHPGaugeImage = GetComponent<Image>();
        }

        UpdateGauge();
    }

    void Update()
    {
        // 常にゲージを更新（リアルタイムで反映）
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        if (bossHPGaugeImage != null && staticScript.BossMaxHP > 0)
        {
            float hpRatio = Mathf.Clamp01(staticScript.BossHP / staticScript.BossMaxHP);
            bossHPGaugeImage.fillAmount = hpRatio;
        }
    }
}