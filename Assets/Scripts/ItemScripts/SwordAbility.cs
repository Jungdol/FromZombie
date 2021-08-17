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

    public bool dotDamage = false;

    public bool isDotDamage = false;
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
     �� �¾Ƶ� ��ȥ�� ������ �ٴ� ��
    ȭ�� (��Ʈ)
    ���� (ü�� ����Ʈ�� ȿ��)
    �� (��Ʈ)
    ���� (��Ʈ..? ���� ȿ���� ����)

    ��Ʈ -> Ʈ���� ��� ���
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

    // ���߿� �����丵 �۾� �� Katana �޼ҵ忡�� �Ű������� byte�� ����� bool �ְ�
    // r, g, b, isDot �� ���� �޼ҵ�� ����Ͽ� ����ȭ �ϱ�
    int FlameKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            StopAllCoroutines();
            StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return dotDamages();
    }

    int ElectricKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            StopAllCoroutines();
            StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return player.status.atkDmg;
    }

    int PosionKatana(bool isDotReturn)
    {
        if (!isDotReturn)
        {
            StopAllCoroutines();
            StartCoroutine(DotDamage());
            return player.status.atkDmg;
        }
        return player.status.atkDmg;
    }

    int LazerKatana()
    {
        dotDamage = false;
        return player.status.atkDmg;
    }

    int BleedKatana()
    {
        player.status.nowHp += player.status.atkDmg / 2;
        return player.status.atkDmg;
    }

    int IceKatana()
    {
        dotDamage = false;
        return player.status.atkDmg;
    }

    int NormalKatana()
    {
        dotDamage = false;
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
        for(int i = 0; i <= 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            isDotDamage = true;
            Debug.Log(i);
            Debug.Log(isDotDamage);
            if (i == 10)
            {
                isDotDamage = false;
                dotDamage = false;
                yield break;
            }
        }
    }
}
