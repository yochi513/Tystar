using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationScript : MonoBehaviour
{
    [SerializeField] Image targetImage;
    [SerializeField] List<Sprite> spritsList;
    [SerializeField] KeyCode nextKey=KeyCode.D;
    [SerializeField] KeyCode BackKey = KeyCode.A;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (spritsList.Count > 0)
        {
            targetImage.sprite=spritsList[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextKey))
        {
            index++;
            if (index >= spritsList.Count)
            {
                index = 0;
            }
            targetImage.sprite=spritsList[index];
        }
        else if (Input.GetKeyDown(BackKey))
        {
            index--;
            if (index <= spritsList.Count)
            {
                index = 0;
            }
            targetImage.sprite = spritsList[index];
        }



    }
}
