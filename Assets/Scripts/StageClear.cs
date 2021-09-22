using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    Collider2D col2D;
    AbilityManager abilityManager;
    DataManager dataManager;
    InGameMgr gameMgr;
    public int stage;
    public Enemy enemy;
    bool isClear;
    BGMManager theBGM;

    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        gameMgr = FindObjectOfType<InGameMgr>();
        abilityManager = FindObjectOfType<AbilityManager>();
        theBGM = FindObjectOfType<BGMManager>();
    }

    void BackLobby()
    {
        dataManager.stage += 1;
        isClear = false;
        abilityManager.abilityPoint++;
        dataManager.SaveData();
        gameMgr.LobbyExit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || !isClear)
        {
            if ((stage == 2 || stage == 3) && enemy.status.nowHp <= 0)
            {
                BackLobby();
            }
            else if (stage == 1)
            {
                BackLobby();
            }
        }
    }
}
