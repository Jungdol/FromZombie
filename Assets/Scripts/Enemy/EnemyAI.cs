using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Player player;

    float attackDelay;
    public float AtkTime;
    [HideInInspector]
    public float distance;
    public int atkNum = 1;

    Enemy enemy;
    Animator enemyAnimator;

    bool isDie = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;

        target = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    
    void FixedUpdate()
    {
        if (AtkTime > 0)
            AtkTime -= Time.deltaTime;

        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= enemy.status.fieldOfVision)
        {
            FaceTarget();

            if (distance <= enemy.status.atkRange)
            {
                if (enemy.unitCode == UnitCode.flyEnemy2 && distance <= enemy.status.atkRange - 1f)
                    FlyingMonster(true);
                else if (!isDie)
                    AttackTarget();
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    if (!isDie)
                        MoveToTarget();
                }
            }
        }
        else
        {
            enemyAnimator.SetBool("Walk", false);
        }
    }

    void MoveToTarget() // ??????? ??????
    {
        if (enemy.status.name == "FlyEnemy1" || enemy.status.name == "FlyEnemy2")
            FlyingMonster();
        else
            GroundMonster();
    }

    void GroundMonster()
    {
        float dir = target.position.x - transform.position.x;
        float dir2;
        dir2 = (dir < 0) ? -1 : 1;

        if (dir < enemy.status.atkRange * -0.1f || dir > enemy.status.atkRange * 0.1f)
        {
            transform.Translate(new Vector2(dir2, 0) * enemy.status.moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("Walk", true);
        }
    }
    
    void FlyingMonster(bool isNear = false)
    {
        float dir = target.position.x - transform.position.x;
        float dir2;
        dir2 = (dir < 0) ? -1 : 1;

        float dir3 = target.position.y - transform.position.y;
        float dir4;
        dir4 = (dir3 < 0) ? -1 : 1;

        if (isNear)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            transform.Translate(new Vector2(dir2, dir4) * enemy.status.moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("Walk", true);
        }

        else if (enemy.unitCode == UnitCode.flyEnemy1 && (dir < enemy.status.atkRange * -0.1f || dir > enemy.status.atkRange * 0.1f || dir3 < enemy.status.atkRange * -0.1f || dir3 > enemy.status.atkRange * 0.1f))
        {
            transform.Translate(new Vector2(dir2, dir4) * enemy.status.moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("Walk", true);
        }

        else if (enemy.unitCode == UnitCode.flyEnemy2 && (dir < enemy.status.atkRange * -0.1f || dir > enemy.status.atkRange * 0.1f || dir3 < enemy.status.atkRange * -0.1f || dir3 > enemy.status.atkRange * 0.1f))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(new Vector2(dir2, dir4) * enemy.status.moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("Walk", true);
        }

    }


    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // ????? ????? ???? ??    
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // ????? ??????? ???? ??
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()
    {
        AtkCombo();
        enemyAnimator.SetTrigger("Attack"); // ???? ??????? ????
        attackDelay = enemy.status.atkSpeed; // ?????? ????
    }

    public void EnemyDelayAttack()
    {
        if (player.hitTime == 0 && distance <= enemy.status.atkRange) // ?? ???? ?????? ???? ??
        {
            target.GetComponent<Player>().status.nowHp -= enemy.status.atkDmg; // ?��?????? ???????? ????.
            InGameMgr.Inst.DamageTxt(enemy.status.atkDmg, target.transform, Color.blue); // ?��???? ?????? ????
            player.isHit = true; // ?��???? ?��???
        }
    }

    public void isBoom() // Enemy1 ?? ???, ????
    {
        isDie = true;
        Invoke("Boom", 0.8f); // 0.8?? ?? ?????? ????.
    }
    
    void Boom()
    {
        if (distance <= enemy.status.atkRange) // ?? ???? ?????? ???? ??
        {
            if (player.hitTime == 0)
            {
                target.GetComponent<Player>().status.nowHp -= enemy.status.atkDmg + 11; // ?��?????? ???????? ????.
                InGameMgr.Inst.DamageTxt(enemy.status.atkDmg, target.transform, Color.blue); // ?��???? ?????? ????
                player.isHit = true;
            }
        }
        this.enabled = false;
    }

    void AtkCombo() // 1~3? ?????? ????.
    {
        if (AtkTime <= 0)
            AtkTime = 0;

        if (AtkTime == 0)
        {
            enemyAnimator.SetFloat("atkCombo", 0);
            AtkTime = enemy.status.atkSpeed + 0.25f;
            atkNum = 1;
        }
            
        else if (AtkTime >= 0)
        {
            enemyAnimator.SetFloat("atkCombo", atkNum++);
            AtkTime = enemy.status.atkSpeed + 0.25f;
            if (atkNum == 3)
                atkNum = 0;
        }
        
    }
}