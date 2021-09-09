using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityKind
{
    A_1, A_2, A_3,
    B_1, B_2, B_3,
    C_1, C_2, C_3,
    D_1, D_2, D_3
}

public class AbilitySet : MonoBehaviour
{
    AbilityManager abilityManager;

    private void Start()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    public bool isApply(bool isTrue)
    {
        if (isTrue)
            return true;
        return false;
    }

    public void SetAbility(AbilityKind abilityKind)
    {
        switch (abilityKind)
        {
            case AbilityKind.A_1: // 공격
                if (abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[0] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.A_2:
                if (abilityManager.nowAbilitys[0] == true && abilityManager.AbilityPoint >= 2)
                {
                    abilityManager.nowAbilitys[1] = true;
                    abilityManager.AbilityPoint -= 2;
                }
                    break;
            case AbilityKind.A_3:
                if (abilityManager.nowAbilitys[1] == true && abilityManager.AbilityPoint >= 3)
                {
                    abilityManager.nowAbilitys[2] = true;
                    abilityManager.AbilityPoint -= 3;
                }
                    break;
            case AbilityKind.B_1: // 체력
                if (abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[3] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.B_2:
                if (abilityManager.nowAbilitys[3] == true && abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[4] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.B_3:
                if (abilityManager.nowAbilitys[4] == true && abilityManager.AbilityPoint >= 2)
                {
                    abilityManager.nowAbilitys[5] = true;
                    abilityManager.AbilityPoint -= 2;
                }
                    break;
            case AbilityKind.C_1: // 내구도
                if (abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[6] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.C_2:
                if (abilityManager.nowAbilitys[5] == true && abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[7] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.C_3:
                if (abilityManager.nowAbilitys[7] == true && abilityManager.AbilityPoint >= 2)
                {
                    abilityManager.nowAbilitys[8] = true;
                    abilityManager.AbilityPoint -= 2;
                }
                    break;
            case AbilityKind.D_1: // 기력
                if (abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[9] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.D_2:
                if (abilityManager.nowAbilitys[9] == true && abilityManager.AbilityPoint >= 1)
                {
                    abilityManager.nowAbilitys[10] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
            case AbilityKind.D_3:
                if (abilityManager.nowAbilitys[10] == true && abilityManager.AbilityPoint >= 2)
                {
                    abilityManager.nowAbilitys[11] = true;
                    abilityManager.AbilityPoint -= 1;
                }
                    break;
        }
    }
    public bool GetAbilityKind(AbilityKind abilityKind)
    {
        bool isTrue = false;
        switch (abilityKind)
        {
            case AbilityKind.A_1:
                if (abilityManager.nowAbilitys[0] == true)
                    isTrue = true;
                break;
            case AbilityKind.A_2:
                if (abilityManager.nowAbilitys[1] == true)
                    isTrue = true;
                break;
            case AbilityKind.A_3:
                if (abilityManager.nowAbilitys[2] == true)
                    isTrue = true;
                break;
            case AbilityKind.B_1:
                if (abilityManager.nowAbilitys[3] == true)
                    isTrue = true;
                break;
            case AbilityKind.B_2:
                if (abilityManager.nowAbilitys[4] == true)
                    isTrue = true;
                break;
            case AbilityKind.B_3:
                if (abilityManager.nowAbilitys[5] == true)
                    isTrue = true;
                break;
            case AbilityKind.C_1:
                if (abilityManager.nowAbilitys[6] == true)
                    isTrue = true;
                break;
            case AbilityKind.C_2:
                if (abilityManager.nowAbilitys[7] == true)
                    isTrue = true;
                break;
            case AbilityKind.C_3:
                if (abilityManager.nowAbilitys[8] == true)
                    isTrue = true;
                break;
            case AbilityKind.D_1:
                if (abilityManager.nowAbilitys[9] == true)
                    isTrue = true;
                break;
            case AbilityKind.D_2:
                if (abilityManager.nowAbilitys[10] == true)
                    isTrue = true;
                break;
            case AbilityKind.D_3:
                if (abilityManager.nowAbilitys[11] == true)
                    isTrue = true;
                break;
        }
        return isTrue;
    }

}
