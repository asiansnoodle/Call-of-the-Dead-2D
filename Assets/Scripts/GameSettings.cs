using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

public static class GameSettings
{
    // default if the player never changes it
    public static Difficulty CurrentDifficulty { get; private set; } = Difficulty.Normal;

    public static void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        Debug.Log($"GameSettings: Difficulty set to {difficulty}");
    }
}
