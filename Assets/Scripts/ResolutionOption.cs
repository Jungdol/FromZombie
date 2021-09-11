using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    public string buttonSound;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    bool isresolutions = false;

    AudioManager theAudio;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        SettingClear();
    }

    public void SettingClear()
    {
        theAudio.Play(buttonSound);

        InitUI();
        ToggleBool();        
    }
    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate >= 60 && (Screen.resolutions[i].width % 16 <= 6 && Screen.resolutions[i].width * 0.5625 >= Screen.resolutions[i].height))
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution options in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = options.width + " X " + options.height + " " + options.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (options.width == Screen.width && options.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
        isresolutions = true;
    }

    public void DropboxOptionChange (int _x)
    {
        resolutionNum = _x;
        if (isresolutions)
            Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);

        theAudio.Play(buttonSound);
    }

    void ToggleBool()
    {
        if (!PlayerPrefs.HasKey("FullScreen"))
        {
            PlayerPrefs.HasKey("FullScreen");
            PlayerPrefs.SetInt("FullScreen", 1);
        }

        bool fullScreenBool = (PlayerPrefs.GetInt("FullScreen") == 1) ? true : false;
        fullscreenBtn.isOn = fullScreenBool;
        screenMode = (PlayerPrefs.GetInt("FullScreen") == 1) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    public void FullScreenBtn(bool _isFull)
    {
        if (_isFull)
            PlayerPrefs.SetInt("FullScreen", 1);
        else
            PlayerPrefs.SetInt("FullScreen", 0);

        theAudio.Play(buttonSound);

        screenMode = PlayerPrefs.GetInt("FullScreen") == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }
}
