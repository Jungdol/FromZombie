using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public static SwordController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SwordController>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("SwordController");
                    instance = instanceContainer.AddComponent<SwordController>();
                }    
            }
            return instance;
        }
    }
    private static SwordController instance;

    public ResetPolyColider2D[] resetPolyColider2Ds;

    public void SwordSetFloat(string animName, float number)
    {
        foreach (ResetPolyColider2D sword in resetPolyColider2Ds)
        {
            sword.anim.SetFloat(animName, number);
        }
    }

    public void SwordSetTrigger(string triggerName)
    {
        foreach (ResetPolyColider2D sword in resetPolyColider2Ds)
        {
            sword.anim.SetTrigger(triggerName);
        }
    }

    public void SwordResetTrigger(string TriggerName)
    {
        foreach (ResetPolyColider2D sword in resetPolyColider2Ds)
        {
            sword.anim.ResetTrigger(TriggerName);
        }
    }

    public void SwordSetBool(string boolName, bool istrue)
    {
        foreach (ResetPolyColider2D sword in resetPolyColider2Ds)
        {
            sword.anim.SetBool(boolName, istrue);
        }
    }
}
