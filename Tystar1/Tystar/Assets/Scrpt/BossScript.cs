using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float HP = 1500;
    private float MaxHP = 1500;
    [SerializeField] float bossTime = 5f;
    [SerializeField] private float damagePerCharge = 0.1f; // CH 0.1消費ごとのダメージ量
    [SerializeField] private Image bossHPGaugeImage;

    void Start()
    {
        if (bossHPGaugeImage == null)
        {
            // GameObjectの名前やタグで検索する方法
            GameObject gaugeObj = GameObject.Find("Bossゲージ本体"); // ★ゲージオブジェクトの名前に合わせて変更
            if (gaugeObj != null)
            {
                bossHPGaugeImage = gaugeObj.GetComponent<Image>();
            }
        }

        //保存されているHPがあれば復元、なければ初期値
        if (staticScript.BossHP > 0)
        {
            HP = staticScript.BossHP;
            MaxHP = staticScript.BossMaxHP;
        }
        else
        {
            // ★追加★ 初回起動時はMaxHPを保存
            MaxHP = HP;
            staticScript.BossMaxHP = MaxHP;
            staticScript.BossHP = HP;
        }

        // ボス戦開始時にステートをBossAttackに変更　るい追加
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.BossAttack;

        Debug.Log("ボス戦開始！ステート: " + PlayerStateScript.CurrentState);

        // CHゲージを満タンにする（デバッグ用）
        //staticScript.SaveCh = 50f;

        //HPゲージを満タンで初期化（デバッグ用）
        UpdateBossHPGauge();


        StartCoroutine(BossTimer());
    }
    // Update is called once per frame
    void Update()
    {
        CheckBossDamage(); //   るい追加
    }

    
    private void CheckBossDamage()//るい追加 
    {
        // ボスフェーズかつエンターキー長押し中
        if (PlayerStateScript.CurrentState == PlayerStateScript.PlayerState.BossAttack
            && Input.GetKey(KeyCode.Return))
        {
            // CHゲージが残っている場合のみダメージ
            if (staticScript.SaveCh > 0)
            {
                // 1フレームあたりのダメージ
                float damage = damagePerCharge;
                HP -= damage;

                //HPを保存
                staticScript.BossHP = HP;

                //HPゲージを更新
                UpdateBossHPGauge();

                // HPが0以下になったら倒す
                if (HP <= 0)
                {
                    OnBossDefeated();
                }
            }
        }
    }

    //HPゲージ更新メソッド
    private void UpdateBossHPGauge()
    {
        if (bossHPGaugeImage != null)
        {
            // HPの割合を計算（0～1の範囲）
            float hpRatio = Mathf.Clamp01(HP / MaxHP);
            // fillAmountを更新（右から左に減る）
            bossHPGaugeImage.fillAmount = hpRatio;
        }
    }

    private void OnBossDefeated()
    {
        //ボス撃破時にHPをリセット
        staticScript.BossHP = 0;
        staticScript.BossMaxHP = 1500f;

        Debug.Log("ボスを倒した!");
        // ボス撃破時の処理（エフェクトなど）
        // TODO: 勝利演出やシーン遷移
        Destroy(gameObject);//スパル
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);

        // ボス戦終了時にステートを元に戻す　るい追加
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            Debug.Log("Boss戦終了！元のシーンに戻ります");
            SceneManager.LoadScene(staticScript.LastSceneName);
        }
        else
        {
            Debug.LogWarning("戻るシーン情報がありません");
        }
    }

}