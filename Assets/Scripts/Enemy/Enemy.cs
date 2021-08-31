using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;
    public GameObject bossCanvas;

    [Header("Player")]
    public Player player;
    public Transform target;
    public PlayerMovement playerMovement;

    [Header("Weapon")]
    public SwordAbility swordAbility;
    public CameraShake camerashake;

    public Rigidbody2D rigid;
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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255, 0, 0, 50);
        Gizmos.DrawSphere(transform.position, status.atkRange);

        Gizmos.color = new Color32(0, 255, 0, 50);
        Gizmos.DrawSphere(transform.position, status.fieldOfVision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.isAtk == true)
            {
                if (hpBarInstantiate) // ?????? ???? ?? ??易? ????
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

                if ((playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Ready") || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Loop")) && atkDelay == 0f) // ???? ???? 3? ?? ???? ?????? ????? ?﹉? ??? ???? ????
                {
                    EnemyHit(1);
                }
                else if ((atkDelay == 0f && !playerMovement.anim.GetBool("isFall")) || playerMovement.anim.GetFloat("atkCombo") != 2 || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_End")) // ??????? 3??? ?????? ???????? ???????? ?? ?? ?? ??? ???? ????
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

    void EnemyHit(int i = 0) // ?取??? ?? ????
    {
        if (swordAbility.SwordType == AllSwordType.iceKatana)
            SwordTypeEffect(AllSwordType.iceKatana);

        Boss1();
        if (status.name != "boss1")
        {
            status.nowHp -= swordAbility.SwordTypeAbility();
            Vector3 dir = target.transform.position - transform.position;
            dir = dir * -1;
            int dir2 = dir.x >= 0.1f ? 1 : -1;
            rigid.velocity = new Vector3(dir2, 0, 0) * 2f;
        }
        dotCount = 0;
        isDotDamage = true;
        camerashake.Shake();
        if (status.name != "boss1" || status.name == "boss1" && !enemyAnimator.GetBool("isTurtle"))
            InGameMgr.Inst.DamageTxt(swordAbility.SwordTypeAbility(), transform, Color.red); // ?? ?????? ????
        

        if (i == 1)
            atkDelay = 0.25f;
    }

    void Boss1()
    {
        if (status.name == "boss1")
        {
            if (status.nowHp <= status.maxHp * 0.45 && !enemyAnimator.GetBool("isTurtle"))
            {
                enemyAnimator.SetBool("isTurtle", true);
                enemyAnimator.SetTrigger("doTurtle");
            }
            else if (status.nowHp <= status.maxHp * 0.6 && status.nowHp >= status.maxHp * 0.45 && enemyAnimator.GetBool("isTurtle"))
            {
                enemyAnimator.SetBool("isTurtle", false);
                enemyAnimator.SetTrigger("doTurtle");
            }
            else if (status.nowHp <= status.maxHp * 0.7 && status.nowHp >= status.maxHp * 0.6 && !enemyAnimator.GetBool("isTurtle"))
            {
                enemyAnimator.SetBool("isTurtle", true);
                enemyAnimator.SetTrigger("doTurtle");
            }

            if (status.name == "boss1" && !enemyAnimator.GetBool("isTurtle"))
                status.nowHp -= swordAbility.SwordTypeAbility();

            else if (status.name == "boss1" && enemyAnimator.GetBool("isTurtle"))
            {
                status.nowHp -= (swordAbility.SwordTypeAbility() * 0.65f);
                InGameMgr.Inst.DamageTxt(swordAbility.SwordTypeAbility() * 0.65f, transform, Color.red); // ?? ?????? ????
            }
        }
    }

    void SwordTypeEffect(AllSwordType _nowSwordType) // ?? ????? - ?? ??????? ??? ??????
    {
        if (_nowSwordType == AllSwordType.flameKatana) // ??? ????
        {
            enemySr.color = new Color32(235, 130, 0, 255);
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(5, "flame"));
            else if (dotCount == 5)
                enemySr.color = new Color32(255, 255, 255, 255);
        }

        else if (_nowSwordType == AllSwordType.posionKatana) // ??? ????
        {
            enemySr.color = new Color32(0, 180, 35, 255);
            if (!isCoroutineRun)
                StartCoroutine(DotDamage(7, "posion"));

            else if (dotCount == 7)
                isDotDamage = false;
        }

        else if (_nowSwordType == AllSwordType.iceKatana) // ????? ????
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
        enemyAnimator.SetTrigger("Die");            // die ??????? ????
        GetComponent<EnemyAI>().enabled = false;    // ???? ??????
        GetComponent<Collider2D>().enabled = false; // ?��? ??????
        Destroy(GetComponent<Rigidbody2D>());       // ??? ??????

        StartCoroutine(FadeIn());                   // ??易? ???????
        Destroy(gameObject, 3);                     // 3???? ????
        Destroy(hpBar.gameObject, 3);               // 3???? ??易? ????

        if (status.name == "Enemy1") // ?????? ??????? ???
        {
            this.GetComponent<EnemyAI>().isBoom();
            GetComponent<EnemyAI>().enabled = true;
        }

        else if (status.name == "flyEnemy1") // ?????? ???????? ???
        {
            Destroy(gameObject, 1.25f);
        }
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }

    IEnumerator FadeIn() // ??易? ?????.
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
                if (status.name == "boss1" && enemyAnimator.GetBool("isTurtle"))
                {
                    status.nowHp -= (flameDotDamage * 0.65f);
                    InGameMgr.Inst.DamageTxt(flameDotDamage * 0.65f , transform, Color.red); // ?? ?????? ????
                }
                else
                {
                    status.nowHp -= flameDotDamage;
                    InGameMgr.Inst.DamageTxt(flameDotDamage, transform, Color.red); // ?? ?????? ????
                }
                
            }
            else if (_Type == "posion")
            {
                status.nowHp -= status.maxHp / 10;
                InGameMgr.Inst.DamageTxt(status.maxHp / 10, transform, Color.red); // ?? ?????? ????
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

    void Awake()
    {
        canvas = GameObject.Find("Hp Canvas");
        bossCanvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player").GetComponent<Player>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        swordAbility = GameObject.Find("Weapon").GetComponent<SwordAbility>();
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        rigid = GetComponent<Rigidbody2D>();

        status = new Status();
        status = status.SetUnitStatus(unitCode);
        SetAttackSpeed(status.atkSpeed);

        enemySr = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
