using UnityEngine;
using UnityEngine.SceneManagement;

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
