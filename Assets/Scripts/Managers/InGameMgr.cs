using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMgr : MonoBehaviour
{
    public GameObject Fade = null;
    public Image FadeImage = null;

    public Image RunImage = null;

    public GameObject GameOver;
    public Image GameOverImage;

    public Player player;

    public Animator heartAnim;

    [Header("-------- DamageText --------")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamageObj = null;
    [Header("-------- Pause --------")]
    public GameObject Pause;

    public static InGameMgr Inst = null;

    GameObject DamageClone;
    DamageText DamageText;
    Vector3 StCacPos;

    Text GameOverText;
    Image ResurrectBtn;
    Image ExitBtn;
    private void Awake()
    {
        Inst = this;
        //  GameOver.SetActive(false);

        StartCoroutine(FadeIn());
        GameOverText = GameOver.transform.GetChild(0).GetComponent<Text>();
        ResurrectBtn = GameOver.transform.GetChild(1).GetComponent<Image>();
        ExitBtn = GameOver.transform.GetChild(2).GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause.activeSelf == true)
                PauseBack();
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

    public void DamageTxt(float _Value, Transform _txtTr, Color _Color) // 데미지 텍스트 출력 메서드

    {
        if (m_DamageObj == null || m_HUD_Canvas == null)
            return;
        DamageClone = (GameObject)Instantiate(m_DamageObj);
        DamageClone.transform.SetParent(m_HUD_Canvas);
        DamageText = DamageClone.GetComponent<DamageText>();
        if (DamageText != null)
            DamageText.InitDamage(_Value, _Color);
        StCacPos = new Vector3(_txtTr.position.x, _txtTr.position.y + 1.15f, 0.0f);
        DamageClone.transform.position = StCacPos;

    }

    public void Resurrect()
    {
        GameOver.SetActive(false);
        player.status.nowHp = player.status.maxHp;
        player.enabled = false;
        player.isHit = true;
        Invoke("PlayerEnbled", 0.1f);
    }

    public void Exit() // 특성 포인트 저장
    {
        LoadingSceneController.LoadScene("TitleScene");
        Time.timeScale = 1; 
    }

    void PlayerEnbled()
    {
        player.enabled = true;
        player.playerMovement.AnimSetTrigger("Resurrect");
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

    public IEnumerator GameOverFadeOut() // 게임오버 화면 출력
    {
        GameOver.SetActive(true);
        heartAnim.SetFloat("speed", 2);

        byte fadeCount = 0;
        while (fadeCount < 200)
        {
            fadeCount += 2;
            yield return new WaitForSeconds(0.05f);
            GameOverImage.color = new Color32(0, 0, 0, fadeCount);
            GameOverText.color = new Color32(255, 255, 255, fadeCount);
            ResurrectBtn.color = new Color32(255, 255, 255, fadeCount);
            ExitBtn.color = new Color32(255, 255, 255, fadeCount);
        }

        if (fadeCount == 200)
        {
            float speed = 2;
            while (speed > 0)
            {
                speed -= 0.005f;
                heartAnim.SetFloat("speed", speed);
                
                yield return new WaitForSeconds(0.1f);
            }

            if (speed < 0)
            {
                speed = 0;
                heartAnim.SetFloat("speed", 0);
            }
                
        }
    }

    // 게임 오버 후 클리어한 챕터를 기준으로 메세지를 보냄.
    // 로비 매니저에 
}
