using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ボール反射パートで使うチャージゲージの表示と値を管理する。</summary>
public class BallCHScrpt : MonoBehaviour
{
    [SerializeField] Image Char;
    [SerializeField] float maxCharge = 50f;
    [SerializeField] float chargeSpeed = 20f;

    float currentCharge = 0f;

    public bool IsMax => currentCharge >= maxCharge;

    public void Charge(bool charging)
    {
        if (charging)
        {
            currentCharge += chargeSpeed * Time.deltaTime;
        }
        else
        {
            currentCharge -= chargeSpeed * Time.deltaTime;
        }

        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        UpdateUI();
    }

    public void ResetCharge()
    {
        currentCharge = 0f;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (Char != null && maxCharge > 0f)
            Char.fillAmount = currentCharge / maxCharge;
    }
}
