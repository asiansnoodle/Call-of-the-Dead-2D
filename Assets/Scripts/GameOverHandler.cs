using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverUI; // assign a panel
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
        Time.timeScale = 0f;
        if (gameOverUI) gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
