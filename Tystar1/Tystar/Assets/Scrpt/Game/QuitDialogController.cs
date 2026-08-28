using UnityEngine;

public class QuitDialogController : MonoBehaviour
{
    [SerializeField] GameObject quitDialog;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
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
}
