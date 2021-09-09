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
     �� �¾Ƶ� ��ȥ�� ������ �ٴ� ��
    ȭ�� (��Ʈ)
    ���� (ü�� ����Ʈ�� ȿ��)
    �� (��Ʈ)
    ���� (��Ʈ..? ���� ȿ���� ����)

    ��Ʈ -> Ʈ���� ��� ���
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

    // ���߿� �����丵 �۾� �� Katana �޼ҵ忡�� �Ű������� byte�� ����� bool �ְ�
    // r, g, b, isDot �� ���� �޼ҵ�� ����Ͽ� ����ȭ �ϱ�
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
        player.status.nowHp += player.status.atkDmg / 10; // �÷��̾� ���ݷ� / 10��ŭ ����
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
