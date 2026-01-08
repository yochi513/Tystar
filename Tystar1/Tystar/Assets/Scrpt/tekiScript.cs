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

    [Header("エフェクト設定")]
    [SerializeField] private GameObject defeatEffectPrefab;
    [SerializeField] private float effectDuration = 2f;

    public enum SPEED
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five
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

            case PlayerStateScript.PlayerState.None:
                break;
        }
    }

    public void Speed()
    {
        switch (speedtime)
        {
            case SPEED.Zero: speed = 1f; break;
            case SPEED.One: speed = 2f; break;
            case SPEED.Two: speed = 3f; break;
            case SPEED.Three: speed = 4f; break;
            case SPEED.Four: speed = 5f; break;
            case SPEED.Five: speed = 10f; break;
        }
    }

    public void GinoAttack()
    {
        // 雷実行中は入力を受け付けない
        if (Lightning.isExecutingChain) return;

        if (Input.GetKey(assignedKey))
        {
            charge.Tystar(+1);
            charge.EntarWithCallback(gameObject, AppEnemy);
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

    // 撃破時に呼ばれる（ChargeScript / Lightning から）
    public void OnDefeat()
    {
        PlayEffect(defeatEffectPrefab);

        if (dango != null)
        {
            dango.OnDestroyed();
        }
    }

    private void PlayEffect(GameObject effectPrefab)
    {
        if (effectPrefab == null) return;

        GameObject effect = Instantiate(
            effectPrefab,
            transform.position,
            Quaternion.identity
        );

 
    }
}
