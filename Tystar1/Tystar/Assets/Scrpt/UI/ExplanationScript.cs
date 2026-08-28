using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>説明用Spriteをキー操作で前後に切り替える。</summary>
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
        if (targetImage == null || spritsList == null || spritsList.Count == 0)
        {
            Debug.LogWarning("ExplanationScript: 表示先Imageまたは説明用Spriteが設定されていません。", this);
            enabled = false;
            return;
        }

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
            if (index < 0)
            {
                index = spritsList.Count - 1;
            }
            targetImage.sprite = spritsList[index];
        }



    }
}
