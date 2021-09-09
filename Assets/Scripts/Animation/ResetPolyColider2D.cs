using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPolyColider2D : MonoBehaviour
{
    public Animator anim;

    Color srColor;
    public SpriteRenderer sr;
    PlayerMovement playerMovement;

    PolygonCollider2D col2D;

    public int count;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col2D = GetComponent<PolygonCollider2D>();
        playerMovement = this.transform.parent.GetComponent<PlayerMovement>();
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

    public void AttackSound(int _count)
    {
        count = _count;
        if (playerMovement.isEnemyHit)
        {
            SettingSound(playerMovement.hitAttackSound_1, playerMovement.hitAttackSound_2, playerMovement.hitAttackSound_3);
            playerMovement.isEnemyHit = false;
        }
        else if (!playerMovement.isEnemyHit)
            SettingSound(playerMovement.emptyAttackSound_1, playerMovement.emptyAttackSound_2, playerMovement.emptyAttackSound_3);

        void SettingSound(string _sound1, string _sound2, string _sound3)
        {
            if (_count == 1)
            {
                playerMovement.theAudio.Stop(_sound3);
                playerMovement.theAudio.Play(_sound1);
            }

            else if (_count == 2)
            {
                playerMovement.theAudio.Stop(_sound1);
                playerMovement.theAudio.Play(_sound2);
            }

            else if (_count == 3)
            {
                playerMovement.theAudio.Stop(_sound2);
                playerMovement.theAudio.Play(_sound3);
            }
        }
        Debug.Log(_count);
    }
}
