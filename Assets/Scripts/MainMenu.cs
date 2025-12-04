using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Game"; 

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame called - would quit in a build.");
    }

    // difficulty selection 
    public void OnEasyDifficulty()
    {
        GameSettings.SetDifficulty(Difficulty.Easy);
    }

    public void OnNormalDifficulty()
    {
        GameSettings.SetDifficulty(Difficulty.Normal);
    }

    public void OnHardDifficulty()
    {
        GameSettings.SetDifficulty(Difficulty.Hard);
    }
}
