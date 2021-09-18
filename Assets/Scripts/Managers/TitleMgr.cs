using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    [Header("타이틀, 버튼")]
    public GameObject title;
    public GameObject buttons;
    [Header("초기화 화면")]
    public GameObject reset;
    [Header("불러오기 오류 화면")]
    public GameObject loadData;
    [Header("설정화면")]
    public GameObject setting;
    [Header("버튼 소리")]
    public string buttonSound;

    AudioManager theAudio;
    DataManager dataManager;
    Animator resetAnim;
    Animator loadDataAnim;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        dataManager = FindObjectOfType<DataManager>();
        resetAnim = reset.GetComponent<Animator>();
        loadDataAnim = loadData.GetComponent<Animator>();
    }

    public void StartBtn()
    {
        theAudio.Play(buttonSound);
        dataManager.CheckData();

        if (!dataManager.isData)
        {
            dataManager.ResetData();
            LoadingSceneController.LoadScene("LobbyScene");
        }
        else
        {
            reset.SetActive(true);
            resetAnim.SetBool("Appear", true);
        }
    }
    
    public void ResetBtn()
    {
        theAudio.Play(buttonSound);
        dataManager.ResetData();
        LoadingSceneController.LoadScene("LobbyScene");
    }

    public void ResetBackBtn()
    {
        theAudio.Play(buttonSound);
        resetAnim.SetBool("Appear", false);
    }

    public void ReStartBtn()
    {
        theAudio.Play(buttonSound);
        dataManager.CheckData();

        if (!dataManager.isData)
        {
            loadData.SetActive(true);
            loadDataAnim.SetBool("Appear", true);
        }
        else
        {
            dataManager.LoadData();
            LoadingSceneController.LoadScene("LobbyScene");
        }
    }

    public void ReStartBackBtn()
    {
        theAudio.Play(buttonSound);
        loadDataAnim.SetBool("Appear", false);
    }

    public void SettingBtn()
    {
        theAudio.Play(buttonSound);

        setting.SetActive(true);
        title.SetActive(false);
        buttons.SetActive(false);
    }

    public void BackBtn()
    {
        theAudio.Play(buttonSound);

        setting.SetActive(false);
        title.SetActive(true);
        buttons.SetActive(true);
    }

    public void ExitBtn()
    {
        theAudio.Play(buttonSound);

        Application.Quit();
    }
}
