using UnityEngine;
using UnityEngine.UI;

/// <summary>共有しているボスHPの値をUIゲージに反映する表示専用コンポーネント。</summary>
public class BossHPGaugeManager : MonoBehaviour
{
    [SerializeField] private Image bossHPGaugeImage;
    private float previousHp = float.NaN;
    private float previousMaxHp = float.NaN;

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
        if (!Mathf.Approximately(previousHp, staticScript.BossHP) ||
            !Mathf.Approximately(previousMaxHp, staticScript.BossMaxHP))
        {
            UpdateGauge();
        }
    }

    private void UpdateGauge()
    {
        if (bossHPGaugeImage != null && staticScript.BossMaxHP > 0)
        {
            float hpRatio = Mathf.Clamp01(staticScript.BossHP / staticScript.BossMaxHP);
            bossHPGaugeImage.fillAmount = hpRatio;
            previousHp = staticScript.BossHP;
            previousMaxHp = staticScript.BossMaxHP;
        }
    }
  public  void HPMAX(float a)
    {
        staticScript.BossHP = a;
    }
}
