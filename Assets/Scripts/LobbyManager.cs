using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public Book BookManager;

    public GameObject BookBtn;
    public GameObject Book;
    public GameObject Desk;
    public GameObject Fade;

    public Image FadeImage;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Fade.SetActive(false);
        BookManager = FindObjectOfType<Book>();
    }

    // ∞‘¿”

    public void KatanaClick()
    {
        StartCoroutine(FadeOut());
        Invoke("ChangeScene", 0.5f);
    }

    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("FightScene");
    }

    public void CollectionBookClick()
    {
        StartCoroutine(FadeOut());
        Invoke("CollectionBook", 0.5f);
    }
    public void CollectionBook()
    {
        StartCoroutine(FadeIn());
        BookBtn.SetActive(false);
        Book.SetActive(true);
        Desk.transform.localScale = new Vector3(2, 2, 2);
    }

    public void Back()
    {
        StopAllCoroutines();
        BookBtn.SetActive(true) ;
        Book.SetActive(false);
        Desk.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator FadeOut()
    {
        Fade.SetActive(true);

        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.05f;
            yield return new WaitForSeconds(0.01f);
            FadeImage.color = new Color(0, 0, 0, fadeCount);
        }
    }

    IEnumerator FadeIn()
    {
        Fade.SetActive(true);

        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            FadeImage.color = new Color(0, 0, 0, fadeCount);
        }
        Fade.SetActive(false);
    }
}
