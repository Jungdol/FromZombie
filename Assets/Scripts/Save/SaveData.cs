using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Stage;
    public int abilityPoint;
    public bool[] nowAbilitys = new bool[12];
    // 0~2 ����, 3~5 ü��, 6~8 ������, 9~11 ���
}
