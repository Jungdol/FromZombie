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
        if (nowAbilitys[0]) // ü��
            status.playerHp = 10;
        if (nowAbilitys[1])
            status.zombieDamageDecrease = 0.03f;
        if (nowAbilitys[2])
            status.bossDamageDecrease = 0.04f;
        if (nowAbilitys[3]) // ������
            addDurability = 1;
        if (nowAbilitys[4])
            addDurability = 2;
        if (nowAbilitys[5])
            addDurability = 3;
        if (nowAbilitys[6]) // ���
            status.playerEnergy = 10;
        if (nowAbilitys[7])
            delEnergy = 5;
        if (nowAbilitys[8])
            skillDelEnergy = 5;
        if (nowAbilitys[10]) // 9���� ���� 3��,  11���� ����
            status.playerAtkDmg = 2;
    }
}
