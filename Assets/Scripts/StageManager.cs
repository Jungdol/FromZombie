using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    DataManager dataManager;

    [Header("Stage1")]
    public GameObject Parallax1;
    public GameObject Stage1;
    [Header("Stage2")]
    public GameObject Parallax2;
    public GameObject Stage2;
    [Header("Stage3")]
    public GameObject Parallax3;
    public GameObject Stage3;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        dataManager.LoadData();
        stageLoad();
    }

    void stageLoad()
    {
        if (dataManager.stage == 0)
        {
            allStageFalse();
            Parallax1.SetActive(true);
            Stage1.SetActive(true);
        }

        else if (dataManager.stage == 1)
        {
            allStageFalse();
            Parallax2.SetActive(true);
            Stage2.SetActive(true);
        }

        else if (dataManager.stage == 2)
        {
            allStageFalse();
            Parallax3.SetActive(true);
            Stage3.SetActive(true);
        }

        void allStageFalse()
        {
            Parallax1.SetActive(false);
            Stage1.SetActive(false);
            Parallax2.SetActive(false);
            Stage2.SetActive(false);
            Parallax3.SetActive(false);
            Stage3.SetActive(false);
        }
    }
}
