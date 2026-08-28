using UnityEngine;

/// <summary>Escapeキーで表示する終了確認ダイアログを、全シーンで一つだけ管理する。</summary>
public class QuitDialogController : MonoBehaviour
{
    [SerializeField] GameObject quitDialog;
    private static QuitDialogController instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (quitDialog == null)
        {
            Debug.LogError("QuitDialogController: quitDialogが設定されていません。", this);
            enabled = false;
            return;
        }
        quitDialog.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleDialog();
        }
    }

    void ToggleDialog()
    {
        bool isActive = quitDialog.activeSelf;
        quitDialog.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f;
    }

    public void OnYes()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnNo()
    {
        quitDialog.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            Time.timeScale = 1f;
        }
    }
}
