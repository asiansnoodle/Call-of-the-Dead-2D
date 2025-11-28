using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager I;
    public TextMeshProUGUI scoreText;

    int score;
    public int Score => score;

    void Awake() { I = this; UpdateUI(); }

    public void Add(int amount = 1)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"Score: {score}";
    }
}
