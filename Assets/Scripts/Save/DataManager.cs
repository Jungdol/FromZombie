using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    [HideInInspector]
    public AbilityManager abilityManager;
    public int stage = 0;
    [HideInInspector]
    public bool isData = false;

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
        abilityManager = GetComponent<AbilityManager>();
        CheckData();
    }

    public void SaveData()
    {
        SaveData save = new SaveData();
        abilityManager.AbilityApply();
        save.nowAbilitys = abilityManager.nowAbilitys;
        save.abilityPoint = abilityManager.abilityPoint;
        save.stage = stage;
        SaveManager.Save(save);
    }

    public void LoadData()
    {
        SaveData save = SaveManager.Load();
        abilityManager.nowAbilitys = save.nowAbilitys;
        abilityManager.abilityPoint = save.abilityPoint;
        stage = save.stage;
        abilityManager.AbilityApply();
    }

    public void CheckData()
    {
        SaveData save = SaveManager.Load();
        if (save == default)
            isData = false;
        else
            isData = true;
    }

    public void ResetData()
    {
        SaveData save = new SaveData();
        abilityManager.AbilityApply();
        System.Array.Clear(save.nowAbilitys, 0, 12);
        save.abilityPoint = 1;
        save.stage = 0;
        SaveManager.Save(save);
    }
}
