using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public AbilityKind abilityKind;
    public AbilitySet abilitySet;
    public AbilityStatus abilityStatus;

    Animator anim;

    private void Start()
    {
        abilitySet = FindObjectOfType<AbilitySet>();
        anim = GetComponent<Animator>();
        anim.SetBool("Appear", abilitySet.GetAbilityKind(abilityKind));
        abilityStatus = new AbilityStatus();

        abilityStatus = abilityStatus.SetAbilityStatus(abilityKind);
    }
    public void Trigger()
    {
        abilitySet.SetAbility(abilityKind);
        anim.SetBool("Appear", abilitySet.GetAbilityKind(abilityKind));
    }
}
