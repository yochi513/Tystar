using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

/// <summary>ボス戦の前後で動画を再生し、難易度に対応した次のシーンへ遷移する。</summary>
public class VideoTransition : MonoBehaviour
{
    [Header("動画設定")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoDisplay;

    [Header("動画ファイル")]
    [SerializeField] private VideoClip bossBeforeVideo;  // ← ボス戦前の動画
    [SerializeField] private VideoClip bossAfterVideo;   // ← ボス戦後の動画

    [Header("難易度別ボスシーン設定（★追加★）")]
    [SerializeField] private string easyBossSceneName = "BossScene";       // Easy難易度のボスシーン名
    [SerializeField] private string normalBossSceneName = "NormalBossScene";   // Normal難易度のボスシーン名
    [SerializeField] private string hardBossSceneName = "HardBossScene";       // Hard難易度のボスシーン名

    [Header("難易度別メインシーン設定（★追加★）")]
    [SerializeField] private string easySceneName = "MainScene";    // Easy難易度のメインシーン名
    [SerializeField] private string normalSceneName = "NormalScene"; // Normal難易度のメインシーン名
    [SerializeField] private string hardSceneName = "HardScene";     // Hard難易度のメインシーン名

    [Header("スキップ設定")]
    [SerializeField] private bool allowSkip = true;
    [SerializeField] private Text skipText;

    private bool isVideoPlaying = false;
    private string nextSceneName;

    // 現在の難易度シーンを保存する静的変数（★追加★）
    public static string currentDifficultyScene = "";

    void Start()
    {
        // 遷移理由を共有状態から読み、再生する動画と行き先を決める。
        // ボス戦前か後かで動画とシーンを切り替え
        if (staticScript.IsGoingToBoss)
        {
            // ボス戦前
            if (bossBeforeVideo != null)
            {
                videoPlayer.clip = bossBeforeVideo;
                Debug.Log("ボス戦前の動画を再生");
            }

            // 保存されている難易度に応じたボスシーンを設定（★修正★）
            nextSceneName = GetBossSceneByDifficulty();
            Debug.Log("次のボスシーン: " + nextSceneName);
        }
        else
        {
            // ボス戦後（★修正★）
            if (bossAfterVideo != null)
            {
                videoPlayer.clip = bossAfterVideo;
                Debug.Log("ボス戦後の動画を再生");
            }

            // 保存されている難易度シーンに戻る（★追加★）
            if (!string.IsNullOrEmpty(currentDifficultyScene))
            {
                nextSceneName = currentDifficultyScene;
                Debug.Log("保存されていた難易度シーンに戻ります: " + nextSceneName);
            }
            // 互換性のため、staticScript.LastSceneNameもチェック
            else if (!string.IsNullOrEmpty(staticScript.LastSceneName))
            {
                nextSceneName = staticScript.LastSceneName;
                Debug.Log("LastSceneNameを使用: " + nextSceneName);
            }
            else
            {
                Debug.LogWarning("戻るシーンが設定されていません！");
            }
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

    // ★追加★ 外部から難易度シーンを保存するメソッド
    public static void SaveCurrentDifficultyScene(string sceneName)
    {
        currentDifficultyScene = sceneName;
        Debug.Log("難易度シーンを保存: " + sceneName);
    }

    // ★追加★ 外部から難易度シーンを保存して動画シーンに移動
    public static void GoToVideoWithDifficulty(string videoSceneName)
    {
        // 現在のシーン名を保存
        currentDifficultyScene = SceneManager.GetActiveScene().name;
        Debug.Log("現在のシーンを保存: " + currentDifficultyScene);

        // 動画シーンに移動
        SceneManager.LoadScene(videoSceneName);
    }

    // ★追加★ 保存されている難易度に応じたボスシーンを取得
    private string GetBossSceneByDifficulty()
    {
        if (string.IsNullOrEmpty(currentDifficultyScene))
        {
            Debug.LogWarning("難易度シーンが保存されていません！デフォルトのEasyボスシーンを使用します");
            return easyBossSceneName;
        }

        // 保存されているシーン名から難易度を判定
        if (currentDifficultyScene.Contains("Easy") || currentDifficultyScene == easySceneName)
        {
            Debug.Log("難易度: Easy → " + easyBossSceneName);
            return easyBossSceneName;
        }
        else if (currentDifficultyScene.Contains("Normal") || currentDifficultyScene == normalSceneName)
        {
            Debug.Log("難易度: Normal → " + normalBossSceneName);
            return normalBossSceneName;
        }
        else if (currentDifficultyScene.Contains("Hard") || currentDifficultyScene == hardSceneName)
        {
            Debug.Log("難易度: Hard → " + hardBossSceneName);
            return hardBossSceneName;
        }
        else
        {
            Debug.LogWarning("難易度を判定できませんでした。Easyボスシーンを使用します");
            return easyBossSceneName;
        }
    }
}
