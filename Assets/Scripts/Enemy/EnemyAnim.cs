using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public EnemyAI enemyAI;
    
    void Start() 
    {
        enemyAI = transform.parent.gameObject.GetComponent <EnemyAI>();
    }
    void Attack()
    {
        enemyAI.EnemyDelayAttack();
    }
}
