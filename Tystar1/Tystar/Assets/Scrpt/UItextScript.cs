using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UItextScript : MonoBehaviour
{

    public Text scoreText;
    public Text countText;
    public Text SCOREText;
    public Text HPText;
    private int score = 0;
    private int count = 0;
    public CHScript cHScript;
    public Playerscrpt playerScript;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        UpscoreText();
        UpcountText();
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
        scoreText.text = $"ÉXÉRÉA:{score}";
        SCOREText.text = $"ÉXÉRÉA:{score}";
    }
    public void UpcountText()
    {
        countText.text = $"ÉMÉmåÇîjêî:{count}";
    }

    public void UpHPText()
    {
        if (playerScript != null && HPText != null)
        {
            HPText.text = playerScript.PlayerHP.ToString();
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