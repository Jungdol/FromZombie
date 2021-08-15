using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAbility : MonoBehaviour
{
    public ItemSetting itemSetting;
    public ItemSetting.AllSwordType SwordType = ItemSetting.AllSwordType.normalKatana;

    // Start is called before the first frame update
    void Awake()
    {
        SwordType = ItemSetting.AllSwordType.normalKatana;
    }

    // Update is called once per frame
    void Update()
    {
        SwordTypeAbility();
    }

    void SwordTypeAbility()
    {
        if (SwordType == ItemSetting.AllSwordType.flameKatana)
            FlameKatana();
        else if (SwordType == ItemSetting.AllSwordType.electricKatana)
            ElectricKatana();
        else if (SwordType == ItemSetting.AllSwordType.posionKatana)
            PosionKatana();
        else if (SwordType == ItemSetting.AllSwordType.lazerKatana)
            LazerKatana();
        else if (SwordType == ItemSetting.AllSwordType.bleedKatana)
            BleedKatana();
        else if (SwordType == ItemSetting.AllSwordType.iceKatana)
            IceKatana();
        else if (SwordType == ItemSetting.AllSwordType.normalKatana)
            NormalKatana();
    }

    void FlameKatana()
    {

    }
    
    void ElectricKatana()
    {

    }

    void PosionKatana()
    {

    }

    void LazerKatana()
    {

    }

    void BleedKatana()
    {

    }

    void IceKatana()
    {

    }

    void NormalKatana()
    {

    }
}
