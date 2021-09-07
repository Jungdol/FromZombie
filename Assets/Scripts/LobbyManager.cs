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

    void OnEnable()
    {
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

        else if (Input.GetKeyDown(KeyCode.Escape))
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
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseExit() // 특성 포인트 저장
    {
        LoadingSceneController.LoadScene("TitleScene");
        Time.timeScale = 1;
    }

    public void KatanaClick()
    {
        StartCoroutine(FadeOut(FadeImage));
        Invoke("ChangeScene", 0.5f);
    }

    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("FightScene");
    }

    public void CollectionBookClick()
    {
        StartCoroutine(FadeOut(FadeImage));
        Invoke("CollectionBook", 0.5f);
    }
    public void CollectionBook()
    {
        StartCoroutine(FadeIn(FadeImage));
        Katana.SetActive(false);
        BookBtn.SetActive(false);
        Book.SetActive(true);
        AbilityBtn.SetActive(false);
        Desk.transform.localScale = new Vector3(2, 2, 2);
    }

    public void BackClick()
    {
        StartCoroutine(FadeOut(FadeImage));
        Invoke("Back", 0.5f);
    }

    public void Back()
    {
        StartCoroutine(FadeIn(FadeImage));
        Katana.SetActive(true);
        BookBtn.SetActive(true);
        Book.SetActive(false);
        AbilityBtn.SetActive(true);
        Desk.transform.localScale = new Vector3(1, 1, 1);
    }

    public void AbilityClick()
    {
        Ability.SetActive(true);
        StartCoroutine(AbilityDelay(true, true, true));
    }

    public void AbilityBack()
    {
        StartCoroutine(AbilityDelay(false, false, false));
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

    IEnumerator AbilityDelay(bool _isAppear, bool _isGroup, bool _isActive)
    {
        AbilityAnimator.SetBool("Appear", _isAppear);
        yield return new WaitForSeconds(0.25f);
        AbilityGroup.SetActive(_isGroup);
        yield return new WaitForSeconds(0.25f);
        Ability.SetActive(_isActive);
    }
}
