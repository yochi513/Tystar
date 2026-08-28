using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>指定時間だけスプラッシュ画面を表示してから選択画面へ進む。</summary>
public class SplashController : MonoBehaviour
{
    [SerializeField] float waitTime = 2.0f;

    void Start()
    {
        Invoke(nameof(GoNext), waitTime);
    }

    void GoNext()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
