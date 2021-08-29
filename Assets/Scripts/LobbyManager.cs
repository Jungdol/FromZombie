using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public Book BookManager;

    public GameObject Katana;
    public GameObject BookBtn;
    public GameObject Book;
    public GameObject Desk;
    public GameObject Fade;

    public Image FadeImage;

    void OnEnable()
    {
        Fade.SetActive(false);
        BookManager = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(1).GetComponent<Book>();
        Book = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }

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
        Katana.SetActive(false);
        BookBtn.SetActive(false);
        Book.SetActive(true);
        Desk.transform.localScale = new Vector3(2, 2, 2);
    }

    public void BackClick()
    {
        StartCoroutine(FadeOut());
        Invoke("Back", 0.5f);
    }

    public void Back()
    {
        StartCoroutine(FadeIn());
        Katana.SetActive(true);
        BookBtn.SetActive(true);
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
