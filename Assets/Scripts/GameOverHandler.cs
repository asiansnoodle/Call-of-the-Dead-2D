using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverUI;            // assign the panel
    public TextMeshProUGUI titleText;        // "Game Over"
    public TextMeshProUGUI statsText;        // "You killed X... / Survived to round Y"

    [Header("References")]
    public WaveManager waveManager;          // drag your WaveManager here

    bool over;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var hp = player.GetComponent<Health>();
        hp.onDeath.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath()
    {
        if (over) return;
        over = true;

        // Fill in the UI text
        if (titleText != null)
            titleText.text = "Game Over";

        int kills = (ScoreManager.I != null) ? ScoreManager.I.Score : 0;
        int wave  = (waveManager != null) ? waveManager.CurrentWave : 0;

        if (statsText != null)
        {
            statsText.text = $"You killed {kills} zombies\n" +
                             $"You survived to round {wave}";
        }

        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
