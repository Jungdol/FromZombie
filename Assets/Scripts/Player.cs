using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool isAtk = false; // PlayerMovement 스크립트에 사용
    [HideInInspector]
    public bool isPlayerDead = false;
    [HideInInspector]
    public bool isHit = false;

    [HideInInspector]
    public float hitTime = 0;

    float hitTimed = 2;

    public Image nowHpbar;

    PlayerMovement playerMovement;
    public InGameMgr inGameMgr;

    SpriteRenderer spriteRenderer;
    public Status status;

    // Start is called before the first frame update
    void Awake()
    {
        status = new Status();
        status = status.SetUnitStatus(UnitCode.player);

        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;

        Die();
        Hit();
    }

    void Hit()
    {
        if (isHit)
        {
            hitTime = hitTimed;
            StartCoroutine(UnHitTime(hitTimed));
            isHit = false;
        }
        if (hitTime >= 0)
            hitTime -= Time.deltaTime;

        if (hitTime <= 0)
            hitTime = 0;
    }

    void Die()
    {
        if (status.nowHp <= 0 && !isPlayerDead)
        {
            isPlayerDead = true;
            playerMovement.enabled = false;
            playerMovement.AnimSetTrigger("Die");
            StartCoroutine(inGameMgr.GameOverFadeOut());
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
