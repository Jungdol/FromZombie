using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Header("화면 흔들림")]
    public Toggle ShakeToggle;

    [Header("오디오 믹서")]
    public AudioMixer mixer;

    [Header("소리")]
    public Slider MasterSoundSlider;
    public Text MasterSoundText;
    public Slider BGMSoundSlider;
    public Text BGMSoundText;
    public Slider EffectSoundSlider;
    public Text EffectSoundText;
    [Header("버튼 소리")]
    public string buttonSound;

    AudioManager theAudio;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        DataCreate();
    }
    void DataCreate()
    {
        ToggleBool();
        SoundInt();
    }

    void ToggleBool()
    {
        theAudio.Play(buttonSound);

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
        theAudio.Play(buttonSound);

        PlayerPrefs.DeleteAll();
        DataCreate();
    }

    public void OnShakeToggle(bool _isBool)
    {
        if (_isBool)
            PlayerPrefs.SetInt("ShakeOn", 1);
        else
            PlayerPrefs.SetInt("ShakeOn", 0);
        theAudio.Play(buttonSound);
    }

    public void MasterSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("MasterSoundVolume", (int)_value);
        MasterSoundText.text = _value.ToString();
        mixer.SetFloat("Master", _value - 80);
    }

    public void BGMSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("BGMSoundVolume", (int)_value);
        BGMSoundText.text = _value.ToString();
        mixer.SetFloat("BGM", _value - 80);
    }

    public void EffectSoundSlide(float _value)
    {
        PlayerPrefs.SetInt("EffectSoundVolume", (int)_value);
        EffectSoundText.text = _value.ToString();
        mixer.SetFloat("SFX", _value - 80);
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
        mixer.SetFloat("Master", PlayerPrefs.GetInt("MasterSoundVolume") - 80);

        BGMSoundSlider.value = PlayerPrefs.GetInt("BGMSoundVolume");
        BGMSoundText.text = PlayerPrefs.GetInt("BGMSoundVolume").ToString();
        mixer.SetFloat("BGM", PlayerPrefs.GetInt("BGMSoundVolume") - 80);

        EffectSoundSlider.value = PlayerPrefs.GetInt("EffectSoundVolume");
        EffectSoundText.text = PlayerPrefs.GetInt("EffectSoundVolume").ToString();
        mixer.SetFloat("SFX", PlayerPrefs.GetInt("EffectSoundVolume") - 80);
    }
}
