using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoTransition : MonoBehaviour
{
    [Header("動画設定")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoDisplay;

    [Header("動画ファイル")]
    [SerializeField] private VideoClip bossBeforeVideo;  // ← ボス戦前の動画
    [SerializeField] private VideoClip bossAfterVideo;   // ← ボス戦後の動画

    [Header("シーン設定")]
    [SerializeField] private string bossSceneName = "BossScene";

    [Header("スキップ設定")]
    [SerializeField] private bool allowSkip = true;
    [SerializeField] private Text skipText;

    private bool isVideoPlaying = false;
    private string nextSceneName;

    void Start()
    {
        // ボス戦前か後かで動画とシーンを切り替え
        if (staticScript.IsGoingToBoss)
        {
            // ボス戦前
            if (bossBeforeVideo != null)
            {
                videoPlayer.clip = bossBeforeVideo;
                Debug.Log("ボス戦前の動画を再生");
            }
            nextSceneName = bossSceneName;
        }
        else
        {
            // ボス戦後
            if (bossAfterVideo != null)
            {
                videoPlayer.clip = bossAfterVideo;
                Debug.Log("ボス戦後の動画を再生");
            }
            nextSceneName = staticScript.LastSceneName;
        }

        SetupVideoPlayer();
        PlayVideo();
    }

    void SetupVideoPlayer()
    {
        if (videoPlayer != null && videoDisplay != null)
        {
            RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
            videoPlayer.targetTexture = renderTexture;
            videoDisplay.texture = renderTexture;
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer または RawImage が設定されていません！");
        }
    }

    void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            isVideoPlaying = true;
        }
    }

    void Update()
    {
        if (allowSkip && isVideoPlaying && Input.anyKeyDown)
        {
            SkipVideo();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void SkipVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        LoadNextScene();
    }

    void LoadNextScene()
    {
        isVideoPlaying = false;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"次のシーンに移動: {nextSceneName}");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("次のシーン名が設定されていません！");
        }
    }
}
