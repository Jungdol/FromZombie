using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    [Header("타이틀, 버튼")]
    public GameObject Title;
    public GameObject Buttons;
    [Header("설정화면")]
    public GameObject Setting;
    [Header("버튼 소리")]
    public string buttonSound;
    AudioManager theAudio;
    DataManager dataManager;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        dataManager = FindObjectOfType<DataManager>();
    }

    public void StartBtn()
    {
        theAudio.Play(buttonSound);
        dataManager.SaveData();
        LoadingSceneController.LoadScene("LobbyScene");
    }

    public void ReStartBtn()
    {
        theAudio.Play(buttonSound);
        dataManager.LoadData();
    }

    public void SettingBtn()
    {
        theAudio.Play(buttonSound);

        Setting.SetActive(true);
        Title.SetActive(false);
        Buttons.SetActive(false);
    }

    public void BackBtn()
    {
        theAudio.Play(buttonSound);

        Setting.SetActive(false);
        Title.SetActive(true);
        Buttons.SetActive(true);
    }

    public void ExitBtn()
    {
        theAudio.Play(buttonSound);

        Application.Quit();
    }
}
