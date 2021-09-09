using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    [Header("Ÿ��Ʋ, ��ư")]
    public GameObject Title;
    public GameObject Buttons;
    [Header("����ȭ��")]
    public GameObject Setting;

    public void StartBtn()
    {
        LoadingSceneController.LoadScene("LobbyScene");
    }

    public void ReStartBtn()
    {

    }

    public void SettingBtn()
    {

        Setting.SetActive(true);
        Title.SetActive(false);
        Buttons.SetActive(false);
    }

    public void BackBtn()
    {
        Setting.SetActive(false);
        Title.SetActive(true);
        Buttons.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
