using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform firePos;
    public GameObject bullet;
    RaycastHit hit;

    EnemyAI enemyAi;

    public bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 6, Color.red);

        firePos.transform.LookAt(enemyAi.target);

        if (Physics.Raycast(firePos.position, firePos.forward, out hit, 0.1f))
        {
            Debug.Log("¿€µø");
            if (isAttack)
            {
                
                Debug.Log(hit.collider.gameObject.tag);
                Instantiate(bullet, firePos.position, firePos.rotation);
                isAttack = false;
            }
        }
    }
}
