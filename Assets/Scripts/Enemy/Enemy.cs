using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;
    public GameObject bossCanvas;

    [Header("Player")]
    public Player player;
    public PlayerMovement playerMovement;

    [Header("Weapon")]
    public SwordAbility swordAbility;
    public CameraShake camerashake;

    public Status status;
    public UnitCode unitCode;

    SpriteRenderer enemySr;

    Image nowHpbar;
    Image grayHpbar;

    public Animator enemyAnimator;

    RectTransform hpBar;

    public float height = 1.7f;

    float atkDelay = 0;

    bool hpBarInstantiate = true;

    bool isDotDie = false;

    int flameDotDamage = 3;

    float iceTime = 0f;

    public int dotCount = 0;

    bool isCoroutineRun = false;

    bool isDotDamage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.isAtk == true)
            {
                if (hpBarInstantiate) // 공격을 맞을 시 체력바 생성
                {
                    if (status.name != "boss1")
                    {
                        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
                        grayHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
                        nowHpbar = hpBar.transform.GetChild(1).GetComponent<Image>();
                        hpBarInstantiate = false;
                    }
                    

                    else
                    {
                        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
                        grayHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
                        nowHpbar = hpBar.transform.GetChild(1).GetComponent<Image>();
                        hpBarInstantiate = false;
                    }
                }

                if ((playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Ready") || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Loop")) && atkDelay == 0f) // 공중 공격 3타 시 루프 때문에 한번만 맞게 하기 위해 설정
                {
                    EnemyHit(1);
                }
                else if ((atkDelay == 0f && !playerMovement.anim.GetBool("isFall")) || playerMovement.anim.GetFloat("atkCombo") != 2 || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_End")) // 공중공격 3타가 끝나는 과정에서 데미지를 한 번 더 주기 위해 설정
                {
                    EnemyHit(0);
                }
                if (status.nowHp <= 0)
                {
                    Die();
                }
            }
        }
    }

    void EnemyHit(int i = 0) // 맞았을 때 실행
    {
        if (swordAbility.SwordType == AllSwordType.iceKatana)
            SwordTypeEffect(AllSwordType.iceKatana);

        status.nowHp -= swordAbility.SwordTypeAbility();
        dotCount = 0;
        isDotDamage = true;
        camerashake.Shake();
        InGameMgr.Inst.DamageTxt(swordAbility.SwordTypeAbility(), transform, Color.red); // 적 데미지 텍스트

        if (i == 1)
            atkDelay = 0.25f;
    }

    void SwordTypeEffect(AllSwordType _nowSwordType) // 검 이펙트 - 적 디버프나 도트 데미지
    {
        if (_nowSwordType == AllSwordType.flameKatana) // 화염 카타나
        {
            enemySr.color = new Color32(235, 130, 0, 255);
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(5, "flame"));
            else if (dotCount == 5)
                enemySr.color = new Color32(255, 255, 255, 255);
        }

        else if (_nowSwordType == AllSwordType.posionKatana) // 맹독 카타나
        {
            enemySr.color = new Color32(0, 180, 35, 255);
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(7, "posion"));

            else if(dotCount == 7)
                isDotDamage = false;
        }

        else if (_nowSwordType == AllSwordType.iceKatana) // 아이스 카타나
        {
            if (iceTime == 0)
            {
                status.moveSpeed /= 2;
                iceTime = 3f;
            }
            else if (iceTime <= 3f)
                iceTime = 3f;

            enemySr.color = new Color32(0, 255, 255, 255);
        }
    }

    void DotDamage()
    {
        
        if (isDotDamage && !isDotDie)
        {
            if (swordAbility.SwordType == AllSwordType.flameKatana)
                SwordTypeEffect(AllSwordType.flameKatana);
            if (swordAbility.SwordType == AllSwordType.posionKatana)
                SwordTypeEffect(AllSwordType.posionKatana);
            else
                isDotDamage = false;
        }

        if (status.nowHp <= 0 && !isDotDie)
        {
            isDotDie = true;
            Die();
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("Die");            // die 애니메이션 실행
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화

        StartCoroutine(FadeIn());                   // 체력바 페이드인
        Destroy(gameObject, 3);                     // 3초후 제거
        Destroy(hpBar.gameObject, 3);               // 3초후 체력바 제거

        if (status.name == "Enemy1") // 죽으면 자폭하는 기능
        {
            this.GetComponent<EnemyAI>().isBoom();
            GetComponent<EnemyAI>().enabled = true;
        }
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }

    IEnumerator FadeIn() // 체력바 사라짐.
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            grayHpbar.color = new Color(grayHpbar.color.r, grayHpbar.color.g, grayHpbar.color.b, fadeCount);
        }
    }

    IEnumerator DotDamage(int _i, string _Type)
    {
        for (dotCount = 0; dotCount <= _i; dotCount++)
        {
            isCoroutineRun = true;
            isDotDamage = true;
            AllSwordType tempSword;
            tempSword = swordAbility.SwordType;

            if (_Type == "flame")
            {
                status.nowHp -= flameDotDamage;
                InGameMgr.Inst.DamageTxt(flameDotDamage, transform, Color.red); // 적 데미지 텍스트
            }
            else if (_Type == "posion")
            {
                status.nowHp -= status.maxHp / 10;
                InGameMgr.Inst.DamageTxt(status.maxHp / 10, transform, Color.red); // 적 데미지 텍스트
            }

            yield return new WaitForSeconds(0.4f);

            if (dotCount == _i)
            {
                isCoroutineRun = false;
                isDotDamage = false;
                enemySr.color = new Color32(255, 255, 255, 255);
                yield break;
            }
        }
    }

    void Start()
    {
        canvas = GameObject.Find("Hp Canvas");
        bossCanvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player").GetComponent<Player>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        swordAbility = GameObject.Find("Weapon").GetComponent<SwordAbility>();
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        status = new Status();
        status = status.SetUnitStatus(unitCode);
        enemySr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        SetAttackSpeed(status.atkSpeed);
    }

    private void Update()
    {
        DotDamage();
        if (iceTime < 0)
        {
            iceTime = 0;
            status.moveSpeed *= 2;
            enemySr.color = new Color32(255, 255, 255, 255);
        }
            
        if (iceTime != 0 && iceTime <= 3)
            iceTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hpBarInstantiate)
        {
            //if (status.name != "boss1")
            //{
                Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
                hpBar.position = _hpBarPos;
            //}
            nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;
        }    

        if (atkDelay != 0)
        {
            if (atkDelay >= 0)
                atkDelay -= Time.deltaTime;
            else if (atkDelay <= 0)
                atkDelay = 0;
        }
    }
}
