using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMgr : MonoBehaviour
{
    public static SwordMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SwordMgr>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("SwordMgr");
                    instance = instanceContainer.AddComponent<SwordMgr>();
                }
            }
            return instance;
        }
    }
    private static SwordMgr instance;

    [System.Serializable]
    public class WeaponAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public WeaponAnimArray[] WeaponArrays;
}

