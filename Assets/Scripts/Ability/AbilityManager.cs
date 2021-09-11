using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private static AbilityManager instance = null;
    Status status;
    
    public int addDurability;
    public int delEnergy;
    public int skillDelEnergy;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        status = GetComponent<Status>();
    }

    public bool[] nowAbilitys = new bool[12];
    public int abilityPoint;

    public void AbilityApply()
    {
        if (nowAbilitys[0]) // 체력
            status.playerHp = 10;
        if (nowAbilitys[1])
            status.zombieDamageDecrease = 0.03f;
        if (nowAbilitys[2])
            status.bossDamageDecrease = 0.04f;
        if (nowAbilitys[3]) // 내구도
            addDurability = 1;
        if (nowAbilitys[4])
            addDurability = 2;
        if (nowAbilitys[5])
            addDurability = 3;
        if (nowAbilitys[6]) // 기력
            status.playerEnergy = 10;
        if (nowAbilitys[7])
            delEnergy = 5;
        if (nowAbilitys[8])
            skillDelEnergy = 5;
        if (nowAbilitys[10]) // 9번은 공중 3단,  11번은 돌진
            status.playerAtkDmg = 2;
    }
}
