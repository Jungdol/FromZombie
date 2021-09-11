using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    public GameObject Player;
    GameObject slot0Item;
    GameObject slot1Item;
    GameObject slot2Item;

    public Transform slot_0;
    public Transform slot_1;
    public Transform slot_2;

    [HideInInspector]
    public Inventory inven;

    public GameObject[] Sword;
    public SwordAbility swordAbility;

    [HideInInspector]
    public string slot0;
    [HideInInspector]
    public string slot1;
    [HideInInspector]
    public bool isAbandonment = true;

    string[] slot0Items = { "Soju", "ElectricWire", "Towel", "ElectricWire", "Towel", "Soju" };
    string[] slot1Items = { "Lighter", "Battery", "Posion", "Lazer", "Linger", "dryIce" };

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

    string ItemName(string _item)
    {
        return _item + "(Clone)";
    }

    bool ItemCheck(string _slot, string _item)
    {
        return _slot == _item ? true : false;
    }
    
    bool FullItemChange(string _slot, string[] _Items)
    {
        bool ItemChecks(string _item)
        {
            return ItemCheck(_slot, ItemName(_item));
        }

        if (ItemChecks(_Items[0]) || ItemChecks(_Items[1]) ||
            ItemChecks(_Items[2]) || ItemChecks(_Items[3]) ||
            ItemChecks(_Items[4]) || ItemChecks(_Items[5]))
            return true;
        else
            return false;
    }
    void SwordSuccess()
    {
        int Count = 0;

        Count += SwordCombination(slot0Items[0], slot1Items[0], AllSwordType.flameKatana); // 화염 카타나
        Count += SwordCombination(slot0Items[1], slot1Items[1], AllSwordType.electricKatana); // 전기 카타나
        Count += SwordCombination(slot0Items[2], slot1Items[2], AllSwordType.posionKatana); // 맹독 카타나
        Count += SwordCombination(slot0Items[3], slot1Items[3], AllSwordType.lazerKatana); // 레이저 카타나
        Count += SwordCombination(slot0Items[4], slot1Items[4], AllSwordType.bleedKatana); // 출혈 카타나
        Count += SwordCombination(slot0Items[5], slot1Items[5], AllSwordType.iceKatana); // 얼음 아카나

        if (Count >= 6 && isAbandonment)
        {
            if (FullItemChange(slot1, slot0Items) && isAbandonment) // slot1 이 slot0아이템 배열과 같으면 slot1을 slot0으로 설정 후 삭제
            {
                slot0 = slot1;
                Destroy(slot1Item);
                isAbandonment = false;

                Count = 0;
            }
                
            else if (FullItemChange(slot0, slot1Items) && isAbandonment) // slot0 이 slot1아이템 배열과 같으면 slot1을 삭제
            {
                Destroy(slot1Item);
                isAbandonment = false;

                Count = 0;
            }
        }
    }

    int SwordCombination(string _item1, string _item2, AllSwordType _changingSwordType)
    {
        if (isTwoCases(ItemName(_item1), ItemName(_item2)))
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
            swordAbility.isDurability = true;
            GameObject ItemImage = Instantiate(Sword[_changingSwordType], inven.slots[2].slotObj.transform, false);
            ItemImage.transform.SetSiblingIndex(ItemImage.transform.GetSiblingIndex() - 1);
            inven.slots[2].isEmpty = false;
        }
    }

    public void Boss3Destroy()
    {
        Destroy(slot0Item);
        Destroy(slot1Item);
        Destroy(slot2Item);
        inven.slots[2].isEmpty = true;

        GameObject ItemImage = Instantiate(Sword[6], inven.slots[2].slotObj.transform, false);
        ItemImage.transform.SetSiblingIndex(ItemImage.transform.GetSiblingIndex() - 1);
        inven.slots[2].isEmpty = false;
    }

    public void SwordReset()
    {

        swordAbility.isDurability = true;
        Destroy(slot2Item);
        GameObject ItemImage = Instantiate(Sword[6], inven.slots[2].slotObj.transform, false);
        ItemImage.transform.SetSiblingIndex(ItemImage.transform.GetSiblingIndex() - 1);
        inven.slots[2].isEmpty = false;
    }

    void SlotNotEmpty()
    {
        if (!inven.slots[0].isEmpty && !inven.slots[1].isEmpty)
            isAbandonment = true;
    }

    private void Update()
    {
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
