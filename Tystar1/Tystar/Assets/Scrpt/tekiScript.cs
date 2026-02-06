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
    public HardEnemySponScript HardEnemy;
    public DangoBehaviorScript dango;

    private Renderer rend;
    private Animator anim;

    [Header("エフェクト設定")]
    [SerializeField] private GameObject defeatEffectPrefab;
    [SerializeField] private float effectDuration = 2f;

    // ================================
    // ★ 撃破状態管理（ここが重要）
    // ================================
    public bool IsDefeated { get; private set; } = false;

    // ★ 撃破を確定させる唯一の入口
    public bool TryDefeat()
    {
        if (IsDefeated) return false;
        IsDefeated = true;
        return true;
    }

    // 撃破エフェクト取得用（外部から参照）
    public GameObject DefeatEffectPrefab => defeatEffectPrefab;

    public enum SPEED
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Normalsix,
        Seven,
        Eight,
        Nine,
        Ten,
        HardEleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
    }

    public SPEED speedtime = SPEED.Five;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");

        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = enemyColor;
        }

        Speed();

        if (charge != null)
        {
            charge.Tystar(0);
        }
    }

    void Update()
    {
        State();

        if (target == null) return;

        transform.LookAt(target.transform);

        float distance = Vector3.Distance(
            transform.position,
            target.transform.position
        );

        if (distance <= attackDistance)
        {
            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }

            speed = 0f;
            return;
        }

        transform.position += transform.forward * speed*Time.deltaTime;
    }

    public void State()
    {
        switch (PlayerStateScript.CurrentState)
        {
            case PlayerStateScript.PlayerState.GinoAttack:
                GinoAttack();
                break;
        }
    }

    public void Speed()
    {
        switch (speedtime)
        {
            case SPEED.Zero:     speed = 1f; break;
            case SPEED.One:      speed = 2f; break;
            case SPEED.Two:      speed = 3f; break;
            case SPEED.Three:    speed = 4f; break;
            case SPEED.Four:     speed = 5f; break;
            case SPEED.Five:    speed = 10f; break;
            case SPEED.Normalsix:  speed=4f; break;
            case SPEED.Seven:   speed = 5.6f;break;
            case SPEED.Eight:     speed = 4f;break;
            case SPEED.Nine:    speed = 7.3f;break;
            case SPEED.HardEleven:speed = 5f;break;
            case SPEED.Twelve:   speed = 8f; break;
            case SPEED.Thirteen: speed = 10f; break;
            case SPEED.Fourteen: speed = 12f; break;
            case SPEED.Fifteen: speed = 15f; break;
        }
    }
   

    public void GinoAttack()
    {
        if (Lightning.isExecutingChain) return;

        if (Input.GetKey(assignedKey))
        {
            charge.Tystar(+1);
            charge.EntarWithCallback(gameObject, AppEnemy,HardEnemy);
            charge.EntarCallback(gameObject, HardEnemy);
        }
        else
        {
            charge.Tystar(-1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Playerscrpt playerHealth = other.GetComponent<Playerscrpt>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }
}
