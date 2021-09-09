using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform firePos;
    public GameObject bullet;
    Vector2 asdf;

    EnemyAI enemyAi;

    public bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        firePos.transform.LookAt(enemyAi.target);
        Debug.DrawRay(firePos.position, firePos.forward * 6, Color.red);

        if (isAttack)
        {
            Instantiate(bullet, firePos.position, firePos.rotation);
            isAttack = false;
        }

        asdf = enemyAi.target.position - transform.position;

    }
}
