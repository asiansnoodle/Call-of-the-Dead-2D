using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

public static class GameSettings
{
    // for handling difficulty
    public static Difficulty CurrentDifficulty { get; private set; } = Difficulty.Normal;

    public static void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        // Debug.Log($"GameSettings: Difficulty set to {difficulty}");
    }

    // option stuff for graphics / visuals
    public static bool Fullscreen { get; private set; } = true;
    public static bool VSyncOn { get; private set; } = true;
    public static int ResolutionIndex { get; private set; } = 0;

    public static float Brightness { get; private set; } = 0f;

    // audio slider stuff
    // 0–1 sliders
    public static float MusicVolume { get; private set; } = 1f;
    public static float SfxVolume { get; private set; } = 1f;

    // options for settings live vsync n stuff
    public static void SetFullscreen(bool value)
    {
        Fullscreen = value;
        Screen.fullScreen = value;
    }

    public static void SetVSync(bool value)
    {
        VSyncOn = value;
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

    public static void SetResolutionIndex(int index)
    {
        ResolutionIndex = index;
    }

    public static void SetBrightness(float value)
    {
        Brightness = Mathf.Clamp01(value);
    }

    // audio options methods:
    public static void SetMusicVolume(float value)
    {
        MusicVolume = Mathf.Clamp01(value);
        if (AudioManager.I != null)
            AudioManager.I.SetMusicVolume(MusicVolume);
    }

    public static void SetSfxVolume(float value)
    {
        SfxVolume = Mathf.Clamp01(value);
        if (AudioManager.I != null)
            AudioManager.I.SetSfxVolume(SfxVolume);
    }

}
