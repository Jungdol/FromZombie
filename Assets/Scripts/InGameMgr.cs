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

    GameObject DamageClone;
    DamageText DamageText;
    Vector3 StCacPos;
    [Header("-------- DamageText --------")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamageObj = null;

    public static InGameMgr Inst = null;

    private void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(this);
        GameOver.SetActive(false);

        StartCoroutine(FadeIn());
    }

    public void DamageTxt(float _Value, Transform _txtTr, Color _Color) // ������ �ؽ�Ʈ ��� �޼���

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

    public IEnumerator GameOverFadeOut() // ���ӿ��� ȭ�� ���
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

    // ���� ���� �� Ŭ������ é�͸� �������� �޼����� ����.
    // �κ� �Ŵ����� 
}
