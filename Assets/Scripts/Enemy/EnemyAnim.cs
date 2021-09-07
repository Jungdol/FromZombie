using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Aiming aiming;
    
    void Start() 
    {
        enemyAI = transform.parent.gameObject.GetComponent <EnemyAI>();
        aiming = transform.parent.gameObject.GetComponent<Aiming>();
    }
    void Attack()
    {
        enemyAI.EnemyDelayAttack();
    }

    void RangedAttack()
    {
        aiming.isAttack = true;
    }
}
