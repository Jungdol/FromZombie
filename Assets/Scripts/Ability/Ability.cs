using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public AbilityKind abilityKind;
    public AbilitySet abilitySet;
    public AbilityStatus abilityStatus;
    public string buttonSound;

    AbilityManager abilityManager;
    DataManager dataManager;
    AudioManager theAudio;

    Animator anim;
    Text abilityPoint;

    private void Start()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
        theAudio = FindObjectOfType<AudioManager>();
        dataManager = FindObjectOfType<DataManager>();
        abilitySet = FindObjectOfType<AbilitySet>();
        
        anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetBool("Appear", abilitySet.GetAbilityKind(abilityKind));

        abilityStatus = new AbilityStatus();
        abilityStatus = abilityStatus.SetAbilityStatus(abilityKind);

        abilityPoint = GetComponent<Text>();
    }
    public void Trigger()
    {
        abilitySet.SetAbility(abilityKind);
        anim.SetBool("Appear", abilitySet.GetAbilityKind(abilityKind));

        if (abilitySet.GetAbilityKind(abilityKind))
        {
            theAudio.Play(buttonSound);
            dataManager.SaveData();
        }
    }

    void PrintAbilityPoint()
    {
        abilityPoint.text = "특성 포인트 : " + abilityManager.abilityPoint + "";
    }

    private void FixedUpdate()
    {
        if (abilityPoint != null)
        {
            PrintAbilityPoint();
        }
    }
}
