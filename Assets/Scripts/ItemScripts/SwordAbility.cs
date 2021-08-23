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

    public int dotCount = 1;

    public bool isDotDamage = false;

    bool isCoroutineRun = false;

    public int nowDurability = 0;
    public int maxDurability = 0;
    public bool isDurability = true;

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
        Debug.Log(nowDurability);
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

    public int SwordTypeAbility(bool isDot = false)
    {
        if (SwordType == AllSwordType.flameKatana)
            return FlameKatana(isDot);
        else if (SwordType == AllSwordType.electricKatana)
            return ElectricKatana(isDot);
        else if (SwordType == AllSwordType.posionKatana)
            return PosionKatana(isDot);
        else if (SwordType == AllSwordType.lazerKatana)
            return LazerKatana(isDot);
        else if (SwordType == AllSwordType.bleedKatana)
            return BleedKatana(isDot);
        else if (SwordType == AllSwordType.iceKatana)
            return IceKatana(isDot);
        else if (SwordType == AllSwordType.normalKatana)
            return NormalKatana(isDot);

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
    int FlameKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            --nowDurability;
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(5));
            return player.status.atkDmg + 5; // 15
        }
        return dotDamages();
    }

    int ElectricKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            --nowDurability;
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(1));
            return player.status.atkDmg + 3; // 13
        }
        return dotDamages();
    }

    int PosionKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            --nowDurability;
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(7));
            return player.status.atkDmg + 3; // 13
        }
        return dotDamages();
    }

    int LazerKatana(bool isDotReturn)
    {
        if (isDotReturn)
            return 0;
        --nowDurability;
        return player.status.atkDmg + 15; // 25
    }

    int BleedKatana(bool isDotReturn)
    {
        if (isDotReturn)
            return 0;
        player.status.nowHp += player.status.atkDmg / 10; // 플레이어 공격력 / 10만큼 흡혈
        --nowDurability;
        return player.status.atkDmg; // 10
    }

    int IceKatana(bool isDotReturn)
    {
        if (isDotReturn)
            return 0;
        --nowDurability;
        return player.status.atkDmg + 1; // 11
    }

    int NormalKatana(bool isDotReturn)
    {
        if (isDotReturn)
            return 0;
        return player.status.atkDmg;
    }

    public int dotDamages()
    {
        if (isDotDamage)
        {
            isDotDamage = false;
            return 1;
        }
        else
            return 0;
    }

    IEnumerator DotDamage(int i)
    {
        for(dotCount = 0; dotCount <= i; dotCount++)
        {
            isCoroutineRun = true;
            AllSwordType tempSword;
            tempSword = SwordType;

            yield return new WaitForSeconds(0.4f);
            isDotDamage = true;
            Debug.Log(dotCount);
            Debug.Log(isDotDamage);

            if (dotCount == 5 || tempSword != SwordType)
            {
                isCoroutineRun = false;
                isDotDamage = false;
                yield break;
            }
        }
    }
    /*
    IEnumerator MovespeedSlowdown()
    {
        
    }*/
}
