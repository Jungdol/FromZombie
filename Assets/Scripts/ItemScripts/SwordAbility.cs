using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllSwordType { normalKatana, flameKatana, electricKatana, posionKatana, lazerKatana, bleedKatana, iceKatana };
public class SwordAbility : MonoBehaviour
{
    public AllSwordType SwordType = AllSwordType.normalKatana;

    public Player player;
    public ItemSetting itemSetting;
    [HideInInspector]
    public SpriteRenderer weaponSr;

    public int i = 0;
    public bool dotDamage = false;

    public bool isDotDamage = false;

    bool isCoroutineRun = false;
    // Start is called before the first frame update
    void Awake()
    {
        SwordType = AllSwordType.normalKatana;
        weaponSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwordType == AllSwordType.flameKatana)
            WeaponColor(255, 75, 0);
        else if (SwordType == AllSwordType.electricKatana)
            WeaponColor(255, 230, 60);
        else if (SwordType == AllSwordType.posionKatana)
            WeaponColor(0, 175, 0);
        else if (SwordType == AllSwordType.lazerKatana)
            WeaponColor(120, 255, 0);
        else if (SwordType == AllSwordType.bleedKatana)
            WeaponColor(255, 0, 0);
        else if (SwordType == AllSwordType.iceKatana)
            WeaponColor(110, 255, 220);
        else if (SwordType == AllSwordType.normalKatana)
            WeaponColor(255, 255, 255);
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

    // 나중에 리펙토링 작업 때 Katana 메소드에서 매개변수에 byte로 색깔과 bool 넣고
    // r, g, b, isDot 로 묶은 메소드로 사용하여 최적화 하기
    int FlameKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            if (!isCoroutineRun)
                StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return dotDamages();
    }

    int ElectricKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            if (!isCoroutineRun)
                StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return dotDamages();
    }

    int PosionKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            if (!isCoroutineRun)
                StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return dotDamages();
    }

    int LazerKatana(bool isDotReturn)
    {
        dotDamage = false;
        if (isDotReturn)
            return 0;
        return player.status.atkDmg;
    }

    int BleedKatana(bool isDotReturn)
    {
        dotDamage = false;
        player.status.nowHp += player.status.atkDmg / 10; // 플레이어 공격력 / 10만큼 흡혈
        if (isDotReturn)
            return 0;
        return player.status.atkDmg;
    }

    int IceKatana(bool isDotReturn)
    {
        dotDamage = false;
        if (isDotReturn)
            return 0;
        return player.status.atkDmg;
    }

    int NormalKatana(bool isDotReturn)
    {
        dotDamage = false;
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

    IEnumerator DotDamage()
    {
        for(i = 0; i <= 10; i++)
        {
            isCoroutineRun = true;
            AllSwordType tempSword;
            tempSword = SwordType;

            yield return new WaitForSeconds(0.2f);
            isDotDamage = true;
            Debug.Log(i);
            Debug.Log(isDotDamage);

            if (i == 10 || tempSword != SwordType)
            {
                isCoroutineRun = false;
                isDotDamage = false;
                dotDamage = false;
                yield break;
            }
        }
    }
}
