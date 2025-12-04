using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Slider brightnessSlider;

    [Header("Audio")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private readonly Vector2Int[] allowedResolutions = new Vector2Int[]
    {
        new Vector2Int(1920, 1080), // default res most common i think
        new Vector2Int(1600, 900),
        new Vector2Int(1366, 768),
        new Vector2Int(1280, 720),
    };

    private void Awake()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < allowedResolutions.Length; i++)
        {
            Vector2Int res = allowedResolutions[i];
            string option = $"{res.x} x {res.y}";
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);

        int indexToUse = GameSettings.ResolutionIndex;

        if (indexToUse < 0 || indexToUse >= allowedResolutions.Length)
        {
            indexToUse = 0;
            for (int i = 0; i < allowedResolutions.Length; i++)
            {
                if (Screen.currentResolution.width == allowedResolutions[i].x &&
                    Screen.currentResolution.height == allowedResolutions[i].y)
                {
                    indexToUse = i;
                    break;
                }
            }

            GameSettings.SetResolutionIndex(indexToUse);
        }

        resolutionDropdown.value = indexToUse;
        resolutionDropdown.RefreshShownValue();
    }

    private void Start()
    {
        fullscreenToggle.isOn = GameSettings.Fullscreen;
        vsyncToggle.isOn      = GameSettings.VSyncOn;

        brightnessSlider.SetValueWithoutNotify(GameSettings.Brightness);

        musicVolumeSlider.SetValueWithoutNotify(GameSettings.MusicVolume);
        sfxVolumeSlider.SetValueWithoutNotify(GameSettings.SfxVolume);
    }


    public void OnResolutionChanged(int index)
    {
        if (index < 0 || index >= allowedResolutions.Length) return;

        Vector2Int res = allowedResolutions[index];
        GameSettings.SetResolutionIndex(index);

        FullScreenMode mode = GameSettings.Fullscreen
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        Screen.SetResolution(res.x, res.y, mode);
    }

    public void OnFullscreenToggled(bool isOn)
    {
        GameSettings.SetFullscreen(isOn);

        int idx = Mathf.Clamp(GameSettings.ResolutionIndex, 0, allowedResolutions.Length - 1);
        Vector2Int res = allowedResolutions[idx];

        FullScreenMode mode = isOn
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        Screen.SetResolution(res.x, res.y, mode);
    }

    public void OnVSyncToggled(bool isOn)
    {
        GameSettings.SetVSync(isOn);
    }

    public void OnBrightnessChanged(float value)
    {
        GameSettings.SetBrightness(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        GameSettings.SetMusicVolume(value);
    }

    public void OnSfxVolumeChanged(float value)
    {
        GameSettings.SetSfxVolume(value);
    }
}
