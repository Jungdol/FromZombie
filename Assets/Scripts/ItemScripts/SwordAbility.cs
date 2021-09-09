using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AllSwordType { normalKatana, flameKatana, electricKatana, posionKatana, lazerKatana, bleedKatana, iceKatana };
public class SwordAbility : MonoBehaviour
{
    public AllSwordType SwordType = AllSwordType.normalKatana;

    public Player player;
    public ItemSetting itemSetting;
    public Image nowisDurabilitybar;
    [HideInInspector]
    public SpriteRenderer weaponSr;

    public int dotCount = 0;

    public bool isDotDamage = false;

    public int nowDurability = 0;
    public int maxDurability = 0;
    public bool isDurability = true;

    public float percentDurability = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SwordType = AllSwordType.normalKatana;
        weaponSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponTypeIndex();
        nowisDurabilitybar.fillAmount = (float)nowDurability / (float)maxDurability;
    }

    /*
     안 맞아도 지혼자 데미지 다는 거
    화염 (도트)
    전기 (체인 라이트닝 효과)
    독 (도트)
    출혈 (도트..? 흡혈 효과면 제외)

    도트 -> 트리거 기능 사용
     */

    public int SwordTypeAbility()
    {
        if (SwordType == AllSwordType.flameKatana)
            return FlameKatana();
        else if (SwordType == AllSwordType.electricKatana)
            return ElectricKatana();
        else if (SwordType == AllSwordType.posionKatana)
            return PosionKatana();
        else if (SwordType == AllSwordType.lazerKatana)
            return LazerKatana();
        else if (SwordType == AllSwordType.bleedKatana)
            return BleedKatana();
        else if (SwordType == AllSwordType.iceKatana)
            return IceKatana();
        else if (SwordType == AllSwordType.normalKatana)
            return NormalKatana();

        return 0;
    }

    void WeaponColor(byte r, byte g, byte b, byte a = 255)
    {
        weaponSr.color = new Color32(r, g, b, a);
    }

    void WeaponDurability(int _input)
    {
        if (isDurability)
        {
            nowDurability = _input;
            maxDurability = _input;
            isDurability = false;
        }
    }

    public void WeaponTypeIndex()
    {
        if (SwordType == AllSwordType.flameKatana)
        {
            WeaponDurability(18);
            WeaponColor(255, 75, 0);
        }

        else if (SwordType == AllSwordType.electricKatana)
        {
            WeaponDurability(18);
            WeaponColor(255, 230, 60);
        }

        else if (SwordType == AllSwordType.posionKatana)
        {
            WeaponDurability(21);
            WeaponColor(0, 175, 0);
        }

        else if (SwordType == AllSwordType.lazerKatana)
        {
            WeaponDurability(5);
            WeaponColor(120, 255, 0);
        }

        else if (SwordType == AllSwordType.bleedKatana)
        {
            WeaponDurability(24);
            WeaponColor(255, 0, 0);
        }

        else if (SwordType == AllSwordType.iceKatana)
        {
            WeaponDurability(21);
            WeaponColor(110, 255, 220);
        }

        if (SwordType == AllSwordType.normalKatana)
        {
            WeaponDurability(1);
            WeaponColor(255, 255, 255);
        }

        if (nowDurability <= 0)
        {
            itemSetting.SwordReset();
            SwordType = AllSwordType.normalKatana;
        }
    }

    // 나중에 리펙토링 작업 때 Katana 메소드에서 매개변수에 byte로 색깔과 bool 넣고
    // r, g, b, isDot 로 묶은 메소드로 사용하여 최적화 하기
    int FlameKatana()
    {
        --nowDurability;
        return player.status.atkDmg + 5; // 15
    }

    int ElectricKatana()
    {
        --nowDurability;
        return player.status.atkDmg + 3; // 13
    }

    int PosionKatana()
    {
        --nowDurability;
        return player.status.atkDmg + 3; // 13
    }

    int LazerKatana()
    {
        --nowDurability;
        return player.status.atkDmg + 15; // 25
    }

    int BleedKatana()
    {
        player.status.nowHp += player.status.atkDmg / 10; // 플레이어 공격력 / 10만큼 흡혈
        --nowDurability;
        return player.status.atkDmg; // 10
    }

    int IceKatana()
    {
        --nowDurability;
        return player.status.atkDmg + 1; // 11
    }

    int NormalKatana()
    {
        return player.status.atkDmg;
    }
}
