using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public Book BookManager;

    [Header("��ư��")]
    public GameObject Katana;
    public GameObject BookBtn;
    public GameObject AbilityBtn;
    [Header("����, å��")]
    public GameObject Book;
    public GameObject Desk;
    [Header("Ư��")]
    public GameObject Ability;
    public GameObject AbilityGroup;
    public Animator AbilityAnimator;
    [Header("���̵� ��, �ƿ�")]
    public GameObject Fade;
    public Image FadeImage;
    [Header("�Ͻ�����")]
    public GameObject Pause;

    BackGroundLoop backGroundLoop;

    void OnEnable()
    {
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

    public void PauseExit() // Ư�� ����Ʈ ����
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
        AllSetActive(false, false, true, false, true);
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
        AllSetActive(true, true, false, true, true);
        Desk.transform.localScale = new Vector3(1, 1, 1);
    }

    public void AbilityClick()
    {
        StartCoroutine(AbilityDelay(true, true, true, true));
    }

    public void AbilityBack()
    {
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
        BookBtn.SetActive(_bookBtn);
        Book.SetActive(_book);
        AbilityBtn.SetActive(_abilityBtn);
        Desk.SetActive(_deskBtn);
    }
}
