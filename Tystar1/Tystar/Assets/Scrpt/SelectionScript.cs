using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button sutez;
    [SerializeField] Button cancel;
    [SerializeField] Button Setumei1;
    [SerializeField] Canvas START;
    [SerializeField] Canvas Select;
    [SerializeField] Canvas Setumei2;

    // Start is called before the first frame update
    void Start()
    {
        Select.gameObject.SetActive(false);
        Setumei2.gameObject.SetActive(false);
        START.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void kaisi()
    {
        Select.gameObject.SetActive(true);
        Setumei2.gameObject.SetActive(false);
        START.gameObject.SetActive(false);

    }
    public void modoru()
    {
        Select.gameObject.SetActive(false);
        Setumei2.gameObject.SetActive(false);
        START.gameObject.SetActive(true);
    }

    public void SELECTEASY()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Setumei()
    {
        Select.gameObject.SetActive(false);
        Setumei2.gameObject.SetActive(true);
    }
    public void Setumeimodoru()
    {
        Select.gameObject.SetActive(true);
        Setumei2.gameObject.SetActive(false);
    }

}
