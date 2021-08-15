using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public GameObject Fade = null;
    public Image FadeImage = null;

    public Image RunImage = null;

    public GameObject GameOver;
    public Image GameOverImage;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameOver.SetActive(false);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Fade.SetActive(true);

        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            FadeImage.color = new Color(0, 0, 0, fadeCount);
            RunImage.color = new Color(255, 255, 255, fadeCount);
        }
        Fade.SetActive(false);
    }

    public IEnumerator GameOverFadeOut()
    {
        GameOver.SetActive(true);

        byte fadeCount = 0;
        while (fadeCount < 200)
        {
            fadeCount += 2;
            yield return new WaitForSeconds(0.01f);
            GameOverImage.color = new Color32(0, 0, 0, fadeCount);
        }
    }

    // 게임 오버 후 클리어한 챕터를 기준으로 메세지를 보냄.
    // 로비 매니저에 
}
