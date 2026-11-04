using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private MonoBehaviour playerMovement;          //kamera ve hareket durur
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    bool paused;

    void Start()
    {
        Time.timeScale = 1f;
        if (pausePanel) pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null || !Keyboard.current.escapeKey.wasPressedThisFrame) return;

        if (paused)
        {
            Resume();
        }
        else
        {
            // Imlec serbestse (ornegin heykelle konusma acik) duraklatma acma
            if (Cursor.lockState != CursorLockMode.Locked) return;
            Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        if (pausePanel) pausePanel.SetActive(true);
        if (playerMovement) playerMovement.enabled = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        paused = false;
        if (pausePanel) pausePanel.SetActive(false);
        if (playerMovement) playerMovement.enabled = true;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GlobalValues.isPlayerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        GlobalValues.isPlayerDead = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}