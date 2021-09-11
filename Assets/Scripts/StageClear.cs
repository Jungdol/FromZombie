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

    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        gameMgr = FindObjectOfType<InGameMgr>();
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || !isClear)
        {
            if ((stage == 2 || stage == 3) && enemy.status.nowHp <= 0)
            {
                dataManager.stage += 1;
                isClear = false;
                abilityManager.abilityPoint++;
                dataManager.SaveData();
                gameMgr.LobbyExit();
            }
            else if (stage == 1)
            {
                dataManager.stage += 1;
                isClear = false;
                abilityManager.abilityPoint++;
                dataManager.SaveData();
                gameMgr.LobbyExit();
            }
        }
    }
}
