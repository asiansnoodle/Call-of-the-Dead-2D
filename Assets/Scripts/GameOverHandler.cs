using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverUI;     // root GameOverUI panel
    [SerializeField] private TextMeshProUGUI titleText; // "GAME OVER"
    [SerializeField] private GameObject hudPanel;       // new HUDPanel parent (HP/Score/Wave/etc.)

    [Header("References")]
    [SerializeField] private WaveManager waveManager;   
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 

    private bool isGameOver;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var hp = player.GetComponent<Health>();
            if (hp != null)
            {
                hp.onDeath.AddListener(OnPlayerDeath);
            }
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (hudPanel != null)
        {
            hudPanel.SetActive(true);
        }
    }

    private void OnPlayerDeath()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (titleText != null)
        {
            titleText.text = "GAME OVER";
        }

        if (hudPanel != null)
        {
            hudPanel.SetActive(false);
        }

        Time.timeScale = 0f;

        if (AudioManager.I != null)
        {
            AudioManager.I.PlayGameOver();
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void OnYesPlayAgain()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void OnNoQuitToMenu()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}
