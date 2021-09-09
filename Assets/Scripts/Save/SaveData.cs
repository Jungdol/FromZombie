using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Stage;
    public int abilityPoint;
    public bool[] nowAbilitys = new bool[12];
    // 0~2 공격, 3~5 체력, 6~8 내구도, 9~11 기력
}
