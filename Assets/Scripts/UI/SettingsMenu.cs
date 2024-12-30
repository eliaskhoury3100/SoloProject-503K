using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;

    int currentResolutionIndex = 0;

    private void Start()
    {
        // Gather Information about Resolutions, Convert to String, Add them to Dropdown
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions(); // Takes string type not Resolution type

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

            options.Add(option);
        }

        resolutionDropdown.AddOptions(options); // Takes list of strings

        // Load saved settings
        LoadSettings();

        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", currentResolutionIndex); // Load saved resolution index
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeExposedParam", volume);
        PlayerPrefs.SetFloat("volume", volume); // Save volume setting
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("graphicsQuality", qualityIndex); // Save graphics quality setting
        PlayerPrefs.Save();
        Debug.Log($"Graphics quality changed to: {QualitySettings.names[qualityIndex]}");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("fullscreen", isFullScreen ? 1 : 0); // Save fullscreen setting
        PlayerPrefs.Save();
    }

    // Updates resolution when a new one is selected
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex); // Save resolution index
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Load saved volume setting and apply
        if (PlayerPrefs.HasKey("volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("volume");
            SetVolume(savedVolume);
        }

        // Load saved graphics quality setting and apply
        if (PlayerPrefs.HasKey("graphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("graphicsQuality");
            SetQuality(savedQuality);
        }

        // Load saved fullscreen setting and apply
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool isFullScreen = PlayerPrefs.GetInt("fullscreen") == 1;
            SetFullScreen(isFullScreen);
        }

        // Load saved resolution setting and apply
        if (PlayerPrefs.HasKey("resolutionIndex"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
            SetResolution(savedResolutionIndex);
        }
    }
}