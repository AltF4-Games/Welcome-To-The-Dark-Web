using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup gameMixer;
    [SerializeField] private TMP_Dropdown resDropdown;
    private List<Resolution> screenResolutions;
    private int currentResolution;

    private void Start()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        screenResolutions = new List<Resolution>(Screen.resolutions.Length);
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            screenResolutions.Add(Screen.resolutions[i]);
            string content = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height;
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.image = null;
            option.text = content;
            options.Add(option);
        }
        resDropdown.AddOptions(options);
        currentResolution = screenResolutions.Count;
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
    }

    public void VolumeSlider(float val)
    {
        gameMixer.audioMixer.SetFloat("Volume", val);
    }

    public void SetGraphicsSettings(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool val)
    {
        Screen.fullScreen = val;
    }

    public void SetResolution(int index)
    {
        int width, height;
        currentResolution = index;
        width = screenResolutions[currentResolution].width;
        height = screenResolutions[currentResolution].height;
        Screen.SetResolution(width,height,Screen.fullScreen);
    }
}