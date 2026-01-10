using System.Collections;
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
    [SerializeField] private ParticleSystem defeatEffect;
    [SerializeField] private Animator animator;

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
        Debug.Log("ボス戦開始! ステート: " + PlayerStateScript.CurrentState);
        Debug.Log("初期チャージ量: " + staticScript.SaveCh);

        //maxHP = HP;
        UpdateHPGauge();

        // CHゲージを満タンにする（デバッグ用）
        //staticScript.SaveCh = 50f;

        //HPゲージを満タンで初期化（デバッグ用）
        UpdateBossHPGauge();


        StartCoroutine(BossTimer());
    }

    void Update()
    {
        CheckBossAttack();
    }

    private void CheckBossAttack()
    {
        Debug.Log($"ステート: {PlayerStateScript.CurrentState}");

        if (PlayerStateScript.CurrentState != PlayerStateScript.PlayerState.BossAttack)
        {
            Debug.Log(" ボス攻撃ステートではありません");
            return;
        }

        Debug.Log($"Enterキー: {Input.GetKey(KeyCode.Return)}, チャージ: {staticScript.SaveCh}");

        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log(" Enterキー押されています");

            if (staticScript.SaveCh > 0)
            {
                Debug.Log(" チャージあり！ダメージ処理開始");

                float damage = damagePerCharge * Time.deltaTime;
                HP -= damage;

                //HPを保存
                staticScript.BossHP = HP;

                //HPゲージを更新
                UpdateBossHPGauge();

                // HPが0以下になったら倒す
                if (HP <= 0)
                {
                    OnBossDefeated();
                    SceneManager.LoadScene("Clear");
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

    // HPゲージ更新メソッド
    private void UpdateHPGauge()
    {
        
        if (bossHPGaugeImage != null)
        {
            bossHPGaugeImage.fillAmount = HP / MaxHP;
        }
        
    }

    private void OnBossDefeated()
    {
        //ボス撃破時にHPをリセット
        staticScript.BossHP = 0;
        staticScript.BossMaxHP = 1500f;

        Debug.Log("ボスを倒した!");

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        //撃破エフェクトを再生
        if (defeatEffect != null)
        {
            defeatEffect.Play();
            Debug.Log("撃破エフェクトを再生しました");
        }

        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        staticScript.IsGoingToBoss = false;  // ← 追加（ボス戦後）

        StartCoroutine(DestroyAndGoToVideo(2f));
    }

    private IEnumerator DestroyAndGoToVideo(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            Debug.Log("ボス撃破！動画シーンへ移動します");
            SceneManager.LoadScene("VideoTransitionScene");
        }

        Destroy(gameObject);
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);

        Debug.Log("時間切れ！動画シーンへ移動します");
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        staticScript.IsGoingToBoss = false;  // ← 追加（ボス戦後）

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            SceneManager.LoadScene("VideoTransitionScene");
        }
        else
        {
            Debug.LogWarning("戻るシーン情報がありません");
        }
    }
}