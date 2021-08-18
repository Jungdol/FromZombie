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
            if (hpBarInstantiate) // ������ ���� �� ü�¹� ����
            {
                hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
                grayHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
                nowHpbar = hpBar.transform.GetChild(1).GetComponent<Image>();
                hpBarInstantiate = false;
            }
            if (player.isAtk == true)
            {
                if ((playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Ready") || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_Loop")) && atkDelay == 0f) // ���� ���� 3Ÿ �� ���� ������ �ѹ��� �°� �ϱ� ���� ����
                {
                    //if (swordAbility.SwordType == AllSwordType.iceKatana) status.moveSpeed *= -2;
                    status.nowHp -= swordAbility.SwordTypeAbility();
                    swordAbility.i = 0;
                    swordAbility.dotDamage = true;
                    InGameMgr.Inst.DamageTxt(player.status.atkDmg, transform, Color.red); // �� ������ �ؽ�Ʈ
                    atkDelay = 0.25f;
                }
                else if ((atkDelay == 0f && !playerMovement.anim.GetBool("isFall")) || playerMovement.anim.GetFloat("atkCombo") != 2 || playerMovement.anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_End")) // ���߰��� 3Ÿ�� ������ �������� �������� �� �� �� �ֱ� ���� ����
                {
                    if (swordAbility.SwordType == AllSwordType.iceKatana) status.moveSpeed /= 2;
                    status.nowHp -= swordAbility.SwordTypeAbility();
                    swordAbility.i = 0;
                    swordAbility.dotDamage = true;
                    InGameMgr.Inst.DamageTxt(player.status.atkDmg, transform, Color.red); // �� ������ �ؽ�Ʈ
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
            InGameMgr.Inst.DamageTxt(player.status.atkDmg / 10, transform, Color.red); // �� ������ �ؽ�Ʈ
        }

        if (status.nowHp <= 0 && !isDotDie)
        {
            isDotDie = true;
            Die();
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("Die");            // die �ִϸ��̼� ����
        GetComponent<EnemyAI>().enabled = false;    // ���� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
        Destroy(GetComponent<Rigidbody2D>());       // �߷� ��Ȱ��ȭ

        StartCoroutine(FadeIn());                   // ü�¹� ���̵���
        Destroy(gameObject, 3);                     // 3���� ����
        Destroy(hpBar.gameObject, 3);               // 3���� ü�¹� ����

        if (status.name == "Enemy1") // ������ �����ϴ� ���
        {
            this.GetComponent<EnemyAI>().isBoom();
            GetComponent<EnemyAI>().enabled = true;
        }
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }

    IEnumerator FadeIn() // ü�¹� �����.
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
