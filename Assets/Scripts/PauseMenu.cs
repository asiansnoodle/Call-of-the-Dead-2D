using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI; 
    [SerializeField] private GameObject hudPanel;    

    [Header("Scenes")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("References")]
    [SerializeField] private GameOverHandler gameOverHandler; 

    private void Start()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        IsPaused = false;
        Time.timeScale = 1f; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverHandler != null && gameOverHandler.IsGameOver)
                return;

            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        if (hudPanel != null)
            hudPanel.SetActive(false); 

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        IsPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (hudPanel != null)
            hudPanel.SetActive(true);

        Time.timeScale = 1f;
    }

    // resume button
    public void OnResumeButton()
    {
        Resume();
    }

    // menu button
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
