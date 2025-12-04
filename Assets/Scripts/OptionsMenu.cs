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

    private Resolution[] resolutions;

    private void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution r = resolutions[i];
            int hz = Mathf.RoundToInt((float)r.refreshRateRatio.value);
            string option = $"{r.width} x {r.height} ({hz}Hz)";

            options.Add(option);

            if (r.width == Screen.currentResolution.width &&
                r.height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        GameSettings.SetResolutionIndex(currentResIndex);
    }

    private void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;

        brightnessSlider.SetValueWithoutNotify(GameSettings.Brightness);

        musicVolumeSlider.SetValueWithoutNotify(GameSettings.MusicVolume);
        sfxVolumeSlider.SetValueWithoutNotify(GameSettings.SfxVolume);
    }


    public void OnResolutionChanged(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;

        Resolution r = resolutions[index];
        bool fullscreen = Screen.fullScreen;

        var refresh = new RefreshRate()
        {
            numerator = r.refreshRateRatio.numerator,
            denominator = r.refreshRateRatio.denominator
        };

        Screen.SetResolution(
            r.width,
            r.height,
            fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
            refresh
        );

        GameSettings.SetResolutionIndex(index);
    }

    public void OnFullscreenToggled(bool isOn)
    {
        GameSettings.SetFullscreen(isOn);
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
