using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>スコア、討伐数、HP、チャージ報酬をまとめて表示するUI窓口。</summary>
public class UItextScript : MonoBehaviour
{

    public Text scoreText;
    public Text countText;
    public Text toGinoText;
    public Text SCOREText;
    public Text HPText;
    private int score = 0;
    private int count = 0;
    private int displayedHp = int.MinValue;
    public CHScript cHScript;
    public Playerscrpt playerScript;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        UpHPText();
    }
    public void SetCount(int newScore, int newCount)
    {
        score = newScore;
        count = newCount;
        UpscoreText();
        UpcountText();
    }
    public void Count(int x, int y)
    {
        // xは今回獲得したスコア、yは同一フレームの撃破数（コンボ数）。
        score += x;
        count += y;
        UpscoreText();
        UpcountText();
        switch (y)
        {
            case 1: cHScript.ChAdd(2); break;
            case 2: cHScript.ChAdd(4); break;
            case 3: cHScript.ChAdd(6); break;
            case 4: cHScript.ChAdd(8); break;
        }
    }
    public void UpscoreText()
    {
        scoreText.text = $"スコア:{score}";
        SCOREText.text = $"スコア:{score}";
    }
    public void UpcountText()
    {
        countText.text = $"ギノ撃破数:{count}";
       // toGinoText.text = $"Bossフェーズまで:{staticScript.SaveMaxGino-count}";
    }

    public void UpHPText()
    {
        if (playerScript != null && HPText != null)
        {
            if (displayedHp != playerScript.PlayerHP)
            {
                displayedHp = playerScript.PlayerHP;
                HPText.text = playerScript.PlayerHP.ToString();
            }
        }
    }
    public void aiu(int a)
    {

        staticScript.SaveKillCount=count;
        staticScript.SaveScore=score;

        score = a;
        count = a;
        cHScript.ChAdd(-100);
    }
}
