using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public GameObject Weapon;
    [HideInInspector]
    public Collider2D col2D;
    [HideInInspector]
    public bool isEnemyHit = false;
    [HideInInspector]
    public float tempSpeed = 0;
    [Header("허공 공격")]
    public string emptyAttackSound_1;
    public string emptyAttackSound_2;
    public string emptyAttackSound_3;
    [Header("공격 피격")]
    public string hitAttackSound_1;
    public string hitAttackSound_2;
    public string hitAttackSound_3;
    [Header("점프")]
    public string jumpSound;
    [Header("대쉬")]
    public string dashSound;

    [HideInInspector]
    public AudioManager theAudio;

    Rigidbody2D rigid2D;

    Player player;

    [HideInInspector]
    public float x = 0;
    float tempX = 0;

    float AtkTime;
    float Idle2Time;
    float DashTime;

    int atkNum = 1;
    int eCount = 0;

    bool isAnim = false;

    bool inputJump = false;

    Transform pos;

    Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        player = GetComponent<Player>();
        pos = GetComponent<Transform>();
        theAudio = FindObjectOfType<AudioManager>();

        Weapon = this.transform.GetChild(0).gameObject;
        //colliders = gameObject.GetComponentsInChildren

        AnimSetFloat("attackSpeed", player.status.atkSpeed);
        AnimSetFloat("AirAttackSpeed", player.status.atkSpeed);
    }

    public void isAnimSetting(int i)
    {
        if (i == 0)
            isAnim = false;
        if (i == 1)
            isAnim = true;
    }

    bool isPlayingAnim (string _animName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(_animName))
            return true;

        return false;
    }

    public void AnimSetTrigger(string _animName)
    {
        if (_animName.Contains("Attack"))
        {
            anim.SetTrigger(_animName);
            SwordController.Instance.SwordSetTrigger(_animName);
        }
        else if (_animName == "Resurrect")
        {
            anim.SetTrigger(_animName);
            SwordController.Instance.SwordSetTrigger(_animName);
        }
        else if (!isPlayingAnim (_animName))
        {
            anim.SetTrigger("do" + _animName);
            SwordController.Instance.SwordSetTrigger("do" + _animName);
        }
    }

    public void AnimResetTrigger(string _animName)
    {
        anim.ResetTrigger(_animName);
        SwordController.Instance.SwordResetTrigger(_animName);
    }

    void AnimSetBool(string _animName, bool _istrue)
    {
        anim.SetBool("is" + _animName, _istrue);
        SwordController.Instance.SwordSetBool("is" + _animName, _istrue);
    }

    void AnimSetFloat(string _floatName, float _number)
    {
        anim.SetFloat(_floatName, _number);
        SwordController.Instance.SwordSetFloat(_floatName, _number);
    }

    #region Atk
    

    public void AirAttackgravityScale(string _state)
    {
        if (_state == "First")
            rigid2D.gravityScale = 0.75f;

        else if (_state == "Second")
            StartCoroutine("AirAtk");
        //rigid2D.gravityScale = -5f;
        else if (_state == "Third")
        {
            rigid2D.gravityScale = 10f;
            AnimResetTrigger("Attack");
        }
        else if (_state == "End")
        {
            StopCoroutine("AirAtk");
            rigid2D.gravityScale = 2f;
        }
    }

    IEnumerator AirAtk()
    {
        rigid2D.velocity = new Vector2(0, 7.5f);
        yield return null;
    }

    public void AtkAnim(int _atkNum)
    {
        AnimSetFloat("atkCombo", _atkNum);
        AnimSetTrigger("Attack");
    }

    public void SetAtk(bool _isStart = false)
    {
        if (_isStart == true)
        {
            AtkAnim(0);
            Idle2Time = 0;
            AnimSetBool("Idle2", true);
        }
        AtkTime = 0;
        player.isAtk = true;
        StartCoroutine("ComboAtk");
    }
    IEnumerator ComboAtk()
    {
        yield return null;
        while (!(Input.GetKeyDown(KeyCode.E)))
        {
            AtkTime += Time.deltaTime;
            yield return null;
        }
        if (AtkTime <= 1f)
        {
            if (!isAnim)
            {
                AtkAnim(atkNum++);
                Idle2Time = 0;
                AnimSetBool("Idle2", true);
            }
            
            if (atkNum <= 3)
            {
                SetAtk();
            }
                
            else
            {
                atkNum = 0;
                player.isAtk = false;
            }
        }
        else
        {
            player.isAtk = false;
            atkNum = 0;
        }
        AtkTime = 0;
    }
    #endregion

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eCount++;
            if (eCount == 3)
                eCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && !player.isAtk && !isAnim)
        {
            SetAtk(true);
            AnimSetBool("Walk", false);
            if (anim.GetBool("isDash"))
                player.hitTime = 0;
        }
        if (atkNum == 3)
        {
            atkNum = 0;
        }
        if ((AtkTime != 0 && AtkTime >= 1f))
        {
            player.isAtk = false;
            atkNum = 1;
            StopCoroutine("ComboAtk");
        }
    }

    void Idle2(string _state = null)
    {
        if (Idle2Time >= 2f && anim.GetBool("isIdle2"))
        {
            AnimSetBool("Idle2", false);
        }
        else if (anim.GetBool("isIdle2"))
            Idle2Time += Time.deltaTime;

        if (_state == "End")
            AnimSetBool("Idle2", false);
    }

    void Jump(string _state)
    {
        if (_state == "Update" && Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isFall") && !isAnim && !anim.GetBool("isDash"))
        {
            inputJump = true;
            theAudio.Play(jumpSound);
        }

        if (_state == "FixedUpdate" && inputJump)
        {
            rigid2D.velocity = Vector2.up * 8.5f;
            
            AnimSetBool("Jump", true);
        }
    }

    void Walk()
    {
        if (x != 0 && (!player.isAtk || AtkTime >= 0.25f) && !isAnim && !anim.GetBool("isDash"))
        {
            AnimSetBool("Walk", true);
            Idle2Time = 0;
        }
        else
            AnimSetBool("Walk", false);

        if ((!player.isAtk || AtkTime >= 0.25f) && !isAnim)
        {
            if (Input.GetKey(KeyCode.LeftControl) && !anim.GetBool("isFall") && !anim.GetBool("isDash")) // Crouch (���̱� + �ȱ�)
            {
                rigid2D.velocity = new Vector2(x * player.status.moveSpeed / 2, rigid2D.velocity.y);
                player.isAtk = false;
                atkNum = 1;
                AnimSetBool("Crouch", true);
                StopCoroutine("ComboAtk");
            }
            else
            {
                rigid2D.velocity = new Vector2(x * player.status.moveSpeed, rigid2D.velocity.y);
                AnimSetBool("Crouch", false);
            }
                
        }
        else
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.LeftArrow) && !isAnim && !anim.GetBool("isDash"))
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            Weapon.transform.eulerAngles = new Vector3(0, 180, 0);
        }
            
        else if (Input.GetKey(KeyCode.RightArrow) && !isAnim && !anim.GetBool("isDash"))
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            Weapon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
            
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.status.nowEnergy > 0 && x != 0 && !isAnim && !anim.GetBool("isDash") && !anim.GetBool("isFall") && !anim.GetBool("isCrouch"))
        {
            theAudio.Play(dashSound);
            player.isEnergyCharge = false;
            player.EnergyTime = 2f;

            player.status.nowEnergy -= 25;
            player.isEnergyCharge = false;
            AnimSetTrigger("Dash");
            player.isDash = true;
            tempSpeed = player.status.moveSpeed;
            player.status.moveSpeed *= 1.25f;
            AnimSetBool("Dash", true);
            Invoke("DashOut", 0.5f);
            x = tempX;
        }
        else if (anim.GetBool("isDash"))
            DashTime += Time.deltaTime;            
    }

    void DashOut()
    {
        AnimSetBool("Dash", false);
        player.status.moveSpeed = tempSpeed;
        DashTime = 0;
        player.isEnergyCharge = false;
        player.EnergyTime = 2f;
    }

    void JumpHit()
    {
        int layerMask = (1 << 8) | (1 << 9); // 8, 9번 레이어만 적용
        RaycastHit2D jumpHit = Physics2D.BoxCast(col2D.bounds.center, new Vector2(0.65f, col2D.bounds.size.y), 0f, Vector2.down, 0.02f, layerMask);

        if (jumpHit.collider != null)
        {
            AnimSetBool("Fall", false);
            AnimSetBool("Jump", false);
        }
            
        else if (jumpHit.collider == null)
        {
            AnimSetBool("Fall", true);
            if (inputJump)
            {
                AnimSetBool("Jump", true);
                inputJump = false;
            }
        }
    }

    private void Update()
    {
        Attack();
        Jump("Update");
        Idle2();
        Dash();
    }

    public void FixedUpdate()
    {
        if (!anim.GetBool("isDash"))
        {
            x = Input.GetAxisRaw("Horizontal");
            tempX = x;
        }

        Walk();
        Jump("FixedUpdate");

        JumpHit();
        treaderRecognition();
    }
    
    void treaderRecognition()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(col2D.bounds.center, new Vector2(0.65f, col2D.bounds.size.y));
    }
}
