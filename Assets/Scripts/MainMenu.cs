using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Game"; // set this in Inspector

    public void PlayGame()
    {
        // Just in case anything was paused in a previous run
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        // This will work in a build
        Application.Quit();

        // Helpful for testing in editor
        Debug.Log("QuitGame called - would quit in a build.");
    }
}
