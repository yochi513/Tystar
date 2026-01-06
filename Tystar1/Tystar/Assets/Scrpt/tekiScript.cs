using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tekiScript : MonoBehaviour
{
    private GameObject target;
    private float speed = 0.05f;
    [SerializeField] private float attackDistance = 1.5f;

    public string word;
    public KeyCode assignedKey;
    public char assignedChar;
    public Color enemyColor;

    public ChargeScript charge;
    public EnemySpponScript AppEnemy;
    public DangoBehaviorScript dango;

    private Renderer rend;

    private Animator anim;

    //= ここから追加 ==========
    [Header("エフェクト設定")]
    [SerializeField] private GameObject defeatEffectPrefab;
    [SerializeField] private GameObject collisionEffectPrefab;
    [SerializeField] private float effectDuration = 2f;
    //ここまで追加

    // private TextMeshPro textDisplay;

    public enum SPEED
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }


    public SPEED speedtime = SPEED.Five;
    public void Speed()
    {
        if (speedtime == SPEED.One)
        {
            speed = 0.02f;
        }
        else if (speedtime == SPEED.Two)
        {
            speed = 0.1f;
        }
        else if (speedtime == SPEED.Three)
        {
            speed = 0.15f;
        }
        else if (speedtime == SPEED.Four)
        {
            speed = 0.2f;
        }
        else if (speedtime == SPEED.Five)
        {
            speed = 0.9f;
        }
        else if (speedtime == SPEED.Zero)
        {
            speed = 0.01f;
        }
    }

    public void State()
    {
        switch (PlayerStateScript.CurrentState)
        {
            case PlayerStateScript.PlayerState.GinoAttack:
                GinoAttack();
                break;
            case PlayerStateScript.PlayerState.None:
                None();
                break;
        }

    }

    void Start()
    {
        anim = GetComponent<Animator>();


        Speed();
        target = GameObject.FindGameObjectWithTag("Player");
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = enemyColor;
        }
        charge.Tystar(0);
    }

    void Update()
    {
        State();
        transform.LookAt(target.transform);
        // ★ プレイヤーとの距離チェック
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= attackDistance)
        {
            // 攻撃アニメーションを再生
            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }

            // 近づきすぎる前に止める（必要なら）
            speed = 0f;

            return; // これ以上前進させない
        }
        transform.position += transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーからPlayerHealthを取得
            Playerscrpt playerHealth = other.GetComponent<Playerscrpt>();
            if (playerHealth != null)
            {
                
             playerHealth.TakeDamage(1); // 敵のダメージ量
            }
        }
    }

    public void GinoAttack()
    {
        if (Lightning.isExecutingChain) return;//雷エフェクトが実行中かどうか
        //  Debug.Log("ギノキー反応");
        if (Input.GetKey(assignedKey))//キー入力を受付してる
        {
            charge.Tystar(+1);
            charge.EntarWithCallback(gameObject, AppEnemy);
        }
        else
        {
            charge.Tystar(-1);
        }
    }
    void BossAttack()
    {
        //  Debug.Log("ボスキー反応");
    }
    void Defense()
    {
    }
    void None()
    {
        // Debug.Log("無効キー反応");
    }
    public bool TryReflect()
    {
        // 書かれたキー(assignedKey)を押した瞬間だけ判定
        return Input.GetKeyDown(assignedKey);
    }
    // 敵が倒されたときに呼び出すメソッド（ChargeScriptから呼ばれる想定）
    public void OnDefeat()
    {   
        // 撃破エフェクトを再生
        PlayEffect(defeatEffectPrefab);
        if (dango != null)
        {
            dango.OnDestroyed();
        }
    }

    // エフェクトを再生するメソッド
    private void PlayEffect(GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration);
        }
    }

}