using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFade : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeInTime = 1.0f;
    [SerializeField] float stayTime = 1.5f;
    [SerializeField] float fadeOutTime = 1.0f;

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // フェードイン（黒→透明）
        yield return Fade(1f, 0f, fadeInTime);

        // 表示時間
        yield return new WaitForSeconds(stayTime);

        // フェードアウト（透明→黒）
        yield return Fade(0f, 1f, fadeOutTime);

        SceneManager.LoadScene("SelectionScene");
    }

    IEnumerator Fade(float from, float to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / time);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
