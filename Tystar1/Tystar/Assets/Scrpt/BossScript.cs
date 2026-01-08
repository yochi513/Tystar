using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public float HP = 10;
    [SerializeField] float maxHP = 10;
    [SerializeField] float bossTime = 30f;
    [SerializeField] private float damagePerSecond = 0.5f;
    [SerializeField] private float hitAnimationInterval = 0.3f;
    [SerializeField] Image hpGaugeImage;

    private Animator animator;
    private float lastHitAnimTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.BossAttack;
        Debug.Log("ボス戦開始! ステート: " + PlayerStateScript.CurrentState);
        Debug.Log("初期チャージ量: " + staticScript.SaveCh);

        maxHP = HP;
        UpdateHPGauge();

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

                float damage = damagePerSecond * Time.deltaTime;
                HP -= damage;

                Debug.Log($" ダメージ! HP: {HP:F2}");

                UpdateHPGauge();

                if (Time.time >= lastHitAnimTime + hitAnimationInterval)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Hit");
                    }
                    lastHitAnimTime = Time.time;
                }

                if (HP <= 0)
                {
                    OnBossDefeated();
                }
            }
            else
            {
                Debug.Log("チャージがありません");
            }
        }
    }

    // HPゲージ更新メソッド
    private void UpdateHPGauge()
    {
        if (hpGaugeImage != null)
        {
            hpGaugeImage.fillAmount = HP / maxHP;
        }
    }

    private void OnBossDefeated()
    {
        Debug.Log("ボスを倒した!");

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        StartCoroutine(DestroyAndReturn(2f));
    }

    private IEnumerator DestroyAndReturn(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            Debug.Log("ボス撃破！元のシーンに戻ります");
            SceneManager.LoadScene(staticScript.LastSceneName);
        }

        Destroy(gameObject);
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTime);

        Debug.Log("時間切れ！元のシーンに戻ります");
        PlayerStateScript.CurrentState = PlayerStateScript.PlayerState.GinoAttack;

        if (!string.IsNullOrEmpty(staticScript.LastSceneName))
        {
            SceneManager.LoadScene(staticScript.LastSceneName);
        }
        else
        {
            Debug.LogWarning("戻るシーン情報がありません");
        }
    }
}