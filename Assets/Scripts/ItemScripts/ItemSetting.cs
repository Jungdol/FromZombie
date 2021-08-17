using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    public GameObject Player;
    GameObject slot0Item;
    GameObject slot1Item;
    GameObject slot2Item;

    Inventory inven;

    public GameObject[] Sword;
    public SwordAbility swordAbility;

    [HideInInspector]
    public string slot0;
    [HideInInspector]
    public string slot1;
    [HideInInspector]
    public bool isAbandonment = true;

    private void Awake()
    {
        inven = Player.GetComponent<Inventory>();
    }

    bool isTwoCases(string _item1, string _item2)
    {
        if (slot0 == _item1 && slot1 == _item2 || slot0 == _item2 && slot1 == _item1)
            return true;
        else
            return false;
    }

    void SwordSuccess()
    {
        int Count = 0;

        Count += SwordCombination("Soju", "Lighter", AllSwordType.flameKatana); // 화염 카타나
        Count += SwordCombination("ElectricWire", "Battery", AllSwordType.electricKatana); // 전기 카타나
        Count += SwordCombination("Towel", "Posion", AllSwordType.posionKatana); // 맹독 카타나
        Count += SwordCombination("ElectricWire", "Lazer", AllSwordType.lazerKatana); // 레이저 카타나
        Count += SwordCombination("Towel", "Linger", AllSwordType.bleedKatana); // 출혈 카타나
        Count += SwordCombination("Soju", "dryIce", AllSwordType.iceKatana); // 얼음 아카나

        if (Count >= 6 && isAbandonment)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && isAbandonment)
            {
                Destroy(slot0Item);
                isAbandonment = false;

                Count = 0;
            }
                
            if (Input.GetKeyDown(KeyCode.Alpha2) && isAbandonment)
            {
                Destroy(slot1Item);
                isAbandonment = false;

                Count = 0;
            }
        }
    }

    int SwordCombination(string _item1, string _item2, AllSwordType _changingSwordType)
    {
        if (isTwoCases(_item1 + "(Clone)", _item2 + "(Clone)"))
        {
            Destroy(slot0Item);
            Destroy(slot1Item);

            Destroy(slot2Item);
            swordAbility.SwordType = _changingSwordType;

            return 0;
        }
        return 1;
    }

    void swordChange()
    {
        if (inven.slots[2].isEmpty == true)
        {
            if (swordAbility.SwordType == AllSwordType.flameKatana)
                SlotItemChange(0);
            else if (swordAbility.SwordType == AllSwordType.electricKatana)
                SlotItemChange(1);
            else if (swordAbility.SwordType == AllSwordType.posionKatana)
                SlotItemChange(2);
            else if (swordAbility.SwordType == AllSwordType.lazerKatana)
                SlotItemChange(3);
            else if (swordAbility.SwordType == AllSwordType.bleedKatana)
                SlotItemChange(4);
            else if (swordAbility.SwordType == AllSwordType.iceKatana)
                SlotItemChange(5);
            else if (swordAbility.SwordType == AllSwordType.normalKatana)
                SlotItemChange(6);
            else
                swordAbility.SwordType = AllSwordType.normalKatana;
        }
    }

    void SlotItemChange(int _changingSwordType)
    {
        if (inven.slots[1].isEmpty)
        {
            Instantiate(Sword[_changingSwordType], inven.slots[2].slotObj.transform, false);
            inven.slots[2].isEmpty = false;
        }
    }

    void SlotNotEmpty()
    {
        if (!inven.slots[0].isEmpty && !inven.slots[1].isEmpty)
        isAbandonment = true;
    }

    private void Update()
    {
        Transform slot_0;
        Transform slot_1;
        Transform slot_2;

        if (this.transform.childCount >= 2)
        {
            slot_0 = transform.GetChild(0);
            slot_1 = transform.GetChild(1);
            slot_2 = transform.GetChild(2);

            if (slot_0.transform.childCount >= 1)
            {
                slot0Item = slot_0.transform.GetChild(0).gameObject;
                slot0 = slot_0.transform.GetChild(0).name;
            }
            else
                slot0 = "";
                
            if (slot_1.transform.childCount >= 1)
            {
                slot1Item = slot_1.transform.GetChild(0).gameObject;
                slot1 = slot_1.transform.GetChild(0).name;
            }
            else
                slot1 = "";

            if (slot_2.transform.childCount >= 1)
            {
                slot2Item = slot_2.transform.GetChild(0).gameObject;
            }
        }

        SlotNotEmpty();
        SwordSuccess();
        swordChange();
    }
}
