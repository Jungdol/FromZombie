using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;
    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilityStatus abilityStatus = GetComponent<Ability>().abilityStatus;

        tooltip.gameObject.SetActive(true);
        tooltip.SetupTooltip(abilityStatus.name, abilityStatus.description, abilityStatus.point);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
