using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    EnemyAI enemyAI;
    Aiming aiming;
    Animator anim;
    ItemSetting itemSetting;

    public GameObject stunParticle = null;
    public GameObject enemy1 = null;
    public GameObject enemy2 = null;

    float tempPlayerSpeed;

    public bool isAnim;
    
    void Start() 
    {
        enemyAI = transform.parent.gameObject.GetComponent <EnemyAI>();
        aiming = transform.parent.gameObject.GetComponent<Aiming>();
        anim = GetComponent<Animator>();
        itemSetting = FindObjectOfType<ItemSetting>();
    }

    void isAnimation(int i)
    {
        isAnim = i == 1 ? true : false;
    }
    void Attack()
    {
        enemyAI.EnemyDelayAttack();
    }

    void RangedAttack()
    {
        aiming.isAttack = true;
    }

    void TP()
    {
        enemyAI.transform.position = enemyAI.target.transform.position;
    }

    void TPed()
    {
        anim.SetBool("isTP", false);
    }

    void Stun()
    {
        tempPlayerSpeed = enemyAI.player.status.moveSpeed;
        enemyAI.player.status.moveSpeed = 0;

        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(0.1f);
        itemSetting.swordAbility.SwordType = AllSwordType.normalKatana;
        itemSetting.Boss3Destroy();
        GameObject stunPar = Instantiate(stunParticle, enemyAI.player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.9f);
        enemyAI.player.status.moveSpeed = tempPlayerSpeed;
        Destroy(stunPar);
    }

    void Summon()
    {
        GameObject Portal = transform.GetChild(0).gameObject;
        Portal.SetActive(true);
        StartCoroutine(SummonTime(Portal));
    }

    IEnumerator SummonTime(GameObject _go)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 7; i++)
        {
            if (Random.Range(0, 2) == 0)
                Instantiate(enemy1, _go.transform.position, Quaternion.identity);
            else
                Instantiate(enemy2, _go.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
        _go.SetActive(false);
    }
}
