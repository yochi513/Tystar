using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoTransition : MonoBehaviour
{
    [Header("動画設定")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoDisplay;

    [Header("シーン設定")]
    [SerializeField] private string nextSceneName = "BossScene";

    [Header("スキップ設定")]
    [SerializeField] private bool allowSkip = true;
    [SerializeField] private Text skipText; // 任意: "Press any key to skip" 表示用

    private bool isVideoPlaying = false;

    void Start()
    {
        SetupVideoPlayer();
        PlayVideo();
    }

    void SetupVideoPlayer()
    {
        if (videoPlayer != null && videoDisplay != null)
        {
            // RenderTextureを作成
            RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
            videoPlayer.targetTexture = renderTexture;
            videoDisplay.texture = renderTexture;

            // 動画終了時のイベント設定
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
        // スキップ機能
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
        SceneManager.LoadScene(nextSceneName);
    }
}