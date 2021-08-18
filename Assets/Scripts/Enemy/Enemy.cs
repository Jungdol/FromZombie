using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;

    public Player player;
    public SwordAbility swordAbility;
    public PlayerMovement playerMovement;

    public Status status;
    public UnitCode unitCode;

    Image nowHpbar;
    Image grayHpbar;

    public Animator enemyAnimator;

    RectTransform hpBar;

    public float height = 1.7f;

    float atkDelay = 0;

    bool hpBarInstantiate = true;

    bool isDotDie = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (hpBarInstantiate) // 공격을 맞을 시 체력바 생성
            {
                hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
                grayHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
                nowHpbar = hpBar.transform.GetChild(1).GetComponent<Image>();
                hpBarInstantiate = false;
            }
            if (player.isAtk == true)
            {
                if ((playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Ready") || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Loop")) && atkDelay == 0f) // 공중 공격 3타 시 루프 때문에 한번만 맞게 하기 위해 설정
                {
                    //if (swordAbility.SwordType == AllSwordType.iceKatana) status.moveSpeed *= -2;
                    status.nowHp -= swordAbility.SwordTypeAbility();
                    swordAbility.i = 0;
                    swordAbility.dotDamage = true;
                    InGameMgr.Inst.DamageTxt(player.status.atkDmg, transform, Color.red); // 적 데미지 텍스트
                    atkDelay = 0.25f;
                }
                else if ((atkDelay == 0f && !playerMovement.anim.GetBool("isFall")) || playerMovement.anim.GetFloat("atkCombo") != 2 || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_End")) // 공중공격 3타가 끝나는 과정에서 데미지를 한 번 더 주기 위해 설정
                {
                    if (swordAbility.SwordType == AllSwordType.iceKatana) status.moveSpeed /= 2;
                    status.nowHp -= swordAbility.SwordTypeAbility();
                    swordAbility.i = 0;
                    swordAbility.dotDamage = true;
                    InGameMgr.Inst.DamageTxt(player.status.atkDmg, transform, Color.red); // 적 데미지 텍스트
                }
                if (status.nowHp <= 0)
                {
                    Die();
                }
            }
        }
    }

    void DotDamage()
    {
        
        if (swordAbility.dotDamage && !isDotDie && swordAbility.SwordTypeAbility(true) != 0)
        {
            Debug.Log(status.nowHp);
            status.nowHp -= player.status.atkDmg / 10;
            InGameMgr.Inst.DamageTxt(player.status.atkDmg / 10, transform, Color.red); // 적 데미지 텍스트
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

    // Start is called before the first frame update
    void Start()
    {
        status = new Status();
        status = status.SetUnitStatus(unitCode);

        SetAttackSpeed(status.atkSpeed);
    }

    private void Update()
    {
        DotDamage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hpBarInstantiate)
        {
            Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = _hpBarPos;
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
