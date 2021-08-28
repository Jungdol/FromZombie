using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    void Start()
    {
        ToggleBool();
        InitUI();
        Debug.Log(PlayerPrefs.GetInt("FullScreen"));
    }
    int OptionNum = 0;
    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate >= 60 && (Screen.resolutions[i].width % 16 == 0 && Screen.resolutions[i].height % 9 == 0))
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
    }

    public void DropboxOptionChange (int _x)
    {
        resolutionNum = _x;
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    void ToggleBool()
    {
        if (!PlayerPrefs.HasKey("FullScreen"))
        {
            PlayerPrefs.HasKey("FullScreen");
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            bool fullScreenBool = (PlayerPrefs.GetInt("FullScreen") == 1) ? true : false;
            fullscreenBtn.isOn = fullScreenBool;
        }
        screenMode = (PlayerPrefs.GetInt("FullScreen") == 1) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void FullScreenBtn(bool _isFull)
    {
        if (_isFull)
            PlayerPrefs.SetInt("FullScreen", 1);
        else
            PlayerPrefs.SetInt("FullScreen", 0);

        screenMode = PlayerPrefs.GetInt("FullScreen") == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Debug.Log(PlayerPrefs.GetInt("FullScreen"));
    }
}
