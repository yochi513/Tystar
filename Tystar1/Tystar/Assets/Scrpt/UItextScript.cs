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
    private int score=0;
    private int count=0;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        UpscoreText();
        UpcountText();
    }

   public void Count(int x ,int y)
    {
        score +=x;
        count +=y;
        UpscoreText();
        UpcountText();
    }
   public void UpscoreText()
    {
        scoreText.text = $"ÉXÉRÉA:{score}";
        SCOREText.text= $"ÉXÉRÉA:{score}";
    }
  public  void UpcountText()
    {
        countText.text = $"ÉMÉmåÇîjêî:{count}";
    }
}
