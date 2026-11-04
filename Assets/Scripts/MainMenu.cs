using UnityEngine;
using UnityEngine.SceneManagement;

// Ana menu: Oyna / Cikis
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "MainScene";   // oyun sahnesinin adi
    [SerializeField] private GameObject mainPanel;

    void Start()
    {
        // Oyun sahnesinde imlec kilitleniyor, menude acik olmali
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (mainPanel) mainPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}