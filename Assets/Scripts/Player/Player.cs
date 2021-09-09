using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool isAtk = false; // PlayerMovement ��ũ��Ʈ�� ���
    [HideInInspector]
    public bool isPlayerDead = false;
    [HideInInspector]
    public bool isHit = false;
    [HideInInspector]
    public bool isDash = false;

    [HideInInspector]
    public float hitTime = 0;
    [HideInInspector]
    public float hitTimed = 2;
    [HideInInspector]
    public float DashTimed = 0.7f;
    [Header("Bar")]
    public Image nowHpbar;
    public Image nowEnergybar;
    public InGameMgr inGameMgr;
    public Status status;

    public PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    public bool isEnergyCharge = true;
    public float EnergyTime = 0;

    public void SavePlayer()
    {
        SaveData save = new SaveData();
        // 스테이지, 특성 포인트, 현재 보유 중인 특성 저장
        SaveManager.Save(save);
    }

    public void LoadPlayer()
    {
        SaveManager.Load();
        // 스테이지, 특성 포인트, 현재 보유 중인 특성 불러오기
    }

    void Awake()
    {
        status = new Status();
        status = status.SetUnitStatus(UnitCode.player);

        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        inGameMgr.heartAnim.SetFloat("speed", 1f);
        playerMovement.enabled = true;
        isPlayerDead = false;
        isAtk = false;
        isDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;

        nowEnergybar.fillAmount = (float)status.nowEnergy / (float)status.maxEnergy;
        Die();
        Hit();

        if (status.nowHp > status.maxHp)
            status.nowHp = status.maxHp;

        if (status.nowEnergy > status.maxEnergy)
            status.nowEnergy = status.maxEnergy;

        if (EnergyTime <= 0)
        {
            if (EnergyTime < 0)
                EnergyTime = 0;
            if (EnergyTime == 0)
                isEnergyCharge = true;
        }

        if (EnergyTime >= 0)
            EnergyTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isEnergyCharge && status.nowEnergy < status.maxEnergy)
            status.nowEnergy++;
    }

    void Hit()
    {
        if (isHit)
        {
            hitTime = hitTimed;
            StartCoroutine(UnHitTime(hitTimed));
            isHit = false;
        }

        else if (isDash)
        {
            hitTime = DashTimed;
            isDash = false;
        }

        if (hitTime >= 0)
        {
            hitTime -= Time.deltaTime;
        }

        if (hitTime <= 0)
            hitTime = 0;
    }

    void Die()
    {
        if (status.nowHp <= 0 && !isPlayerDead)
        {
            isPlayerDead = true;
            StartCoroutine(inGameMgr.GameOverFadeOut());
            playerMovement.enabled = false;
            playerMovement.AnimSetTrigger("Die");
        }
    }

    IEnumerator UnHitTime(float hitTime)
    {
        for (int i = 0; i < hitTime * 10; ++i)
        {
            if (i % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);
        }

        //Alpha Effect End
        spriteRenderer.color = new Color32(255, 255, 255, 255);

        yield return null;
    }
}
