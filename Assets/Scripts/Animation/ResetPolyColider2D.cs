using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPolyColider2D : MonoBehaviour
{
    public Animator anim;

    Color srColor;
    public SpriteRenderer sr;

    PolygonCollider2D col2D;


    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col2D = GetComponent<PolygonCollider2D>();
    }

    void ColliderResetAndEnabled(string state)
    {
        if (state == "Enabled")
        {
            col2D.enabled = true;
        }
        else if (state == "Disabled")
        {
            ColiderResetSetting();
            col2D.enabled = false;
        }
    }
    IEnumerator ResetCollider()
    {
        Destroy(col2D);
        yield return 0;
        col2D = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        col2D.isTrigger = true;
    }

    void ColiderResetSetting()
    {
        Destroy(col2D);
        col2D = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        col2D.isTrigger = true;
    }
}
