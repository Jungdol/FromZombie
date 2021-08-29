using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    [Header("Ÿ��Ʋ, ��ư")]
    public GameObject Title;
    public GameObject Buttons;
    [Header("���ȭ��")]
    public GameObject background;
    public GameObject[] bgs;
    [Header("����ȭ��")]
    public GameObject Setting;

    float speed = -2f;
    float endPos = -28.8f; // �Ѻ��� ���Ƽ� ���� ����̴ϱ� �ʱ� ��ġ�� ���� ������ �ȴ�.
    bool isIns = false;


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

    void Start()
    {
        bgs = new GameObject[2];
        bgs[0] = Instantiate(background, new Vector3(28.8f, 0, 0), Quaternion.identity);
    }
    void Update()
    {
        bgs[0].transform.Translate(speed * Time.deltaTime, 0, 0);
        if (bgs[0].transform.position.x <= endPos)
        {
            if (!isIns)
            {
                bgs[1] = Instantiate(background, new Vector3(48f, 0, 0), Quaternion.identity);
                isIns = true;
            }
            bgs[1].transform.Translate(speed * Time.deltaTime, 0, 0);

            if (bgs[0].transform.position.x <= -48.0)
            {
                Destroy(bgs[0]);
                bgs[0] = bgs[1];
                bgs[1] = null;

                isIns = false;
            }
        }
    }
}
