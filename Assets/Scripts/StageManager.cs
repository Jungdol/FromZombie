using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    DataManager dataManager;
    Player player;

    [Header("Stage_Tutorial")]
    public GameObject Parallax_Tutorial;
    public GameObject Stage_Tutorial;

    [Header("Stage1")]
    public GameObject Parallax1;
    public GameObject Stage1;

    [Header("Stage2")]
    public GameObject Parallax2;
    public GameObject Stage2;

    [Header("Stage3")]
    public GameObject Parallax3;
    public GameObject Stage3;

    BGMManager theBGM;

    void Start()
    {
        player = FindObjectOfType<Player>();
        dataManager = FindObjectOfType<DataManager>();
        theBGM = FindObjectOfType<BGMManager>();
        dataManager.LoadData();
        stageLoad();
    }

    void stageLoad()
    {
        player.transform.position = new Vector2(-15, 0);

        switch (dataManager.stage)
        {
            case 0:
                allStageFalse();
                Parallax_Tutorial.SetActive(true);
                Stage_Tutorial.SetActive(true);
                theBGM.Play(0);
                break;
            case 1:
                allStageFalse();
                Parallax1.SetActive(true);
                Stage1.SetActive(true);
                theBGM.Play(1);
                break;
            case 2:
                allStageFalse();
                Parallax2.SetActive(true);
                Stage2.SetActive(true);
                theBGM.Play(2);
                break;
            case 3:
                allStageFalse();
                Parallax3.SetActive(true);
                Stage3.SetActive(true);
                theBGM.Play(3);
                break;
        }
            
        void allStageFalse()
        {
            Parallax_Tutorial.SetActive(false);
            Stage_Tutorial.SetActive(false);

            Parallax1.SetActive(false);
            Stage1.SetActive(false);

            Parallax2.SetActive(false);
            Stage2.SetActive(false);
            
            Parallax3.SetActive(false);
            Stage3.SetActive(false);
        }
    }
}
