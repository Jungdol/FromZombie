using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle ShakeToggle;
    [Header("¼Ò¸®")]
    public Slider MasterSoundSlider;
    public Text MasterSoundText;
    public Slider BGMSoundSlider;
    public Text BGMSoundText;
    public Slider EffectSoundSlider;
    public Text EffectSoundText;

    void Start()
    {
        DataCreate();
    }
    void DataCreate()
    {
        ToggleBool();
        SoundInt();
    }
    void ToggleBool()
    {
        if (!PlayerPrefs.HasKey("ShakeOn"))
        {
            PlayerPrefs.HasKey("ShakeOn");
            PlayerPrefs.SetInt("ShakeOn", 1);
        }
        bool ShakeBool = (PlayerPrefs.GetInt("ShakeOn") == 1) ? true : false;
        ShakeToggle.isOn = ShakeBool;
    }

    public void SettingClear()
    {
        PlayerPrefs.DeleteAll();
        DataCreate();
    }

    public void OnShakeToggle(bool _isBool)
    {
        if (_isBool)
            PlayerPrefs.SetInt("ShakeOn", 1);
        else
            PlayerPrefs.SetInt("ShakeOn", 0);
    }

    public void MasterSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("MasterSoundVolume", (int)_value);
        MasterSoundText.text = _value.ToString();
    }

    public void BGMSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("BGMSoundVolume", (int)_value);
        BGMSoundText.text = _value.ToString();
    }

    public void EffectSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("EffectSoundVolume", (int)_value);
        EffectSoundText.text = _value.ToString();
    }

    void SoundInt()
    {
        if (!PlayerPrefs.HasKey("MasterSoundVolume"))
        {
            PlayerPrefs.HasKey("MasterSoundVolume");
            PlayerPrefs.SetInt("MasterSoundVolume", 50);
        }
        if (!PlayerPrefs.HasKey("BGMSoundVolume"))
        {
            PlayerPrefs.HasKey("BGMSoundVolume");
            PlayerPrefs.SetInt("BGMSoundVolume", 100);
        }
        if (!PlayerPrefs.HasKey("EffectSoundVolume"))
        {
            PlayerPrefs.HasKey("EffectSoundVolume");
            PlayerPrefs.SetInt("EffectSoundVolume", 100);
        }

        MasterSoundSlider.value = PlayerPrefs.GetInt("MasterSoundVolume");
        MasterSoundText.text = PlayerPrefs.GetInt("MasterSoundVolume").ToString();

        BGMSoundSlider.value = PlayerPrefs.GetInt("BGMSoundVolume");
        BGMSoundText.text = PlayerPrefs.GetInt("BGMSoundVolume").ToString();

        EffectSoundSlider.value = PlayerPrefs.GetInt("EffectSoundVolume");
        EffectSoundText.text = PlayerPrefs.GetInt("EffectSoundVolume").ToString();
    }
}
