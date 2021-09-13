using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public Book BookManager;

    [Header("버튼들")]
    public GameObject Katana;
    public GameObject BookBtn;
    public GameObject AbilityBtn;
    [Header("도감, 책상")]
    public GameObject Book;
    public GameObject Desk;
    [Header("특성")]
    public GameObject Ability;
    public GameObject AbilityGroup;
    public Animator AbilityAnimator;
    [Header("페이드 인, 아웃")]
    public GameObject Fade;
    public Image FadeImage;
    [Header("일시정지")]
    public GameObject Pause;
    [Header("버튼 소리")]
    public string buttonSound;

    BackGroundLoop backGroundLoop;

    AudioManager theAudio;

    DataManager dataManager;

    void OnEnable()
    {
        dataManager = FindObjectOfType<DataManager>();
        theAudio = FindObjectOfType<AudioManager>();

        backGroundLoop = GetComponent<BackGroundLoop>();
        Fade.SetActive(false);
        BookManager = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(1).GetComponent<Book>();
        Book = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
    }

    private void Update()
    {
        if (Ability.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            AbilityBack();

        else if (Book.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            BackClick();

        else if (!Ability.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause.activeSelf == true)
            {
                PauseBack();
            }
            else
            {
                Pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void PauseBack()
    {
        theAudio.Play(buttonSound);
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseExit() // 특성 포인트 저장
    {
        theAudio.Play(buttonSound);
        dataManager.SaveData();
        LoadingSceneController.LoadScene("TitleScene");
        Time.timeScale = 1;

    }

    public void KatanaClick()
    {
        theAudio.Play(buttonSound);
        StartCoroutine(FadeOut(FadeImage));
        Invoke("ChangeScene", 0.5f);
    }

    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("FightScene");
    }

    public void CollectionBookClick()
    {
        /*
        theAudio.Play(buttonSound);
        StartCoroutine(FadeOut(FadeImage));
        Invoke("CollectionBook", 0.5f);
        */
    }
    public void CollectionBook()
    {
        StartCoroutine(FadeIn(FadeImage));
        AllSetActive(false, false, true, false, true);
        Desk.transform.localScale = new Vector3(2, 2, 2);
    }

    public void BackClick()
    {
        theAudio.Play(buttonSound);
        StartCoroutine(FadeOut(FadeImage));
        Invoke("Back", 0.5f);
    }

    public void Back()
    {
        StartCoroutine(FadeIn(FadeImage));
        AllSetActive(true, true, false, true, true);
        Desk.transform.localScale = new Vector3(1, 1, 1);
    }

    public void AbilityClick()
    {
        theAudio.Play(buttonSound);
        StartCoroutine(AbilityDelay(true, true, true, true));
    }

    public void AbilityBack()
    {
        theAudio.Play(buttonSound);
        StartCoroutine(AbilityDelay(false, false, true, false));
    }

    IEnumerator FadeOut(Image _image)
    {
        Fade.SetActive(true);

        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.05f;
            yield return new WaitForSeconds(0.01f);
            _image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    IEnumerator FadeIn(Image _image)
    {
        Fade.SetActive(true);

        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            _image.color = new Color(0, 0, 0, fadeCount);
        }
        Fade.SetActive(false);
    }

    IEnumerator AbilityDelay(bool _isStart, bool _isAppear, bool _isGroup, bool _isActive)
    {
        if (_isStart)
        {
            backGroundLoop.isLoop = true;
            StartCoroutine(FadeOut(FadeImage));
            yield return new WaitForSeconds(0.5f);

            AllSetActive(false, false, false, false, false);

            StartCoroutine(FadeIn(FadeImage));
            Ability.SetActive(true);
            AbilityGroup.SetActive(true);
        }

        AbilityAnimator.SetBool("Appear", _isAppear);
        yield return new WaitForSeconds(0.25f);

        if (!_isStart)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeOut(FadeImage));
            yield return new WaitForSeconds(0.5f);

            AllSetActive(true, true, false, true, true);
            StartCoroutine(FadeIn(FadeImage));
            backGroundLoop.isLoop = false;
        }
    }

    void AllSetActive(bool _katanaBtn, bool _bookBtn, bool _book, bool _abilityBtn, bool _deskBtn)
    {
        Katana.SetActive(_katanaBtn);
        //BookBtn.SetActive(_bookBtn);
        Book.SetActive(_book);
        AbilityBtn.SetActive(_abilityBtn);
        Desk.SetActive(_deskBtn);
    }
}
