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
    public bool isSlide = false;

    [HideInInspector]
    public float hitTime = 0;
    [HideInInspector]
    public float hitTimed = 2;
    [HideInInspector]
    public float SlideTimed = 0.7f;

    public Image nowHpbar;
    public Animator heartAnim;
    public InGameMgr inGameMgr;
    public Status status;

    public PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        status = new Status();
        status = status.SetUnitStatus(UnitCode.player);

        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        heartAnim.SetFloat("speed", 1f);
        playerMovement.enabled = true;
        isPlayerDead = false;
        isAtk = false;
        isSlide = false;
    }

    // Update is called once per frame
    void Update()
    {
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;

        Die();
        Hit();

        if (status.nowHp > status.maxHp)
            status.nowHp = status.maxHp;
    }

    void Hit()
    {
        if (isHit)
        {
            hitTime = hitTimed;
            StartCoroutine(UnHitTime(hitTimed));
            isHit = false;
        }

        else if (isSlide)
        {
            hitTime = SlideTimed;
            isSlide = false;
        }

        if (status.nowHp <= 20 && status.nowHp > 0)
            heartAnim.SetFloat("speed", 2f);

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
            StartCoroutine(HeartbeatSlowdown());
            StartCoroutine(inGameMgr.GameOverFadeOut());
            playerMovement.enabled = false;
            playerMovement.AnimSetTrigger("Die");
        }
    }

    IEnumerator HeartbeatSlowdown()
    {
        float speed = 2f;
        while (speed > 0.0f)
        {
            speed -= 0.005f;
            heartAnim.SetFloat("speed", speed);
            yield return new WaitForSeconds(0.01f);
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
