using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    SpriteRenderer renderer;

    ResetPolyColider2D resetPolyColider2D;
    public GameObject Weapon;

    Rigidbody2D rigid2D;
    Collider2D col2D;

    Player player;

    float x = 0;
    float tempX = 0;

    float AtkTime;
    float Idle2Time;
    float SlideTime;

    int atkNum = 1;

    bool isAnim = false;
    bool isSlide = false;

    bool inputJump = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        player = GetComponent<Player>();

        resetPolyColider2D = GetComponentInChildren<ResetPolyColider2D>();
        Weapon = this.transform.GetChild(0).gameObject;

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
            if (isAnim == false)
            {
                AtkAnim(atkNum++);
                Idle2Time = 0;
                AnimSetBool("Idle2", true);
            }
                
            if (atkNum <= 3)
                SetAtk();
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
        if (Input.GetKeyDown(KeyCode.E) && !player.isAtk && !isAnim && !isSlide)
        {
            SetAtk(true);
            AnimSetBool("Walk", false);
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
        if (_state == "Update" && Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isFall") && !isAnim && !isSlide)
            inputJump = true;

        if (_state == "FixedUpdate" && inputJump)
        {
            rigid2D.velocity = Vector2.up * 8.5f;
            AnimSetBool("Jump", true);
        }
    }

    void Walk()
    {
        if (x != 0 && (!player.isAtk || AtkTime >= 0.25f) && !isAnim && !isSlide)
        {
            AnimSetBool("Walk", true);
            Idle2Time = 0;
        }
        else
            AnimSetBool("Walk", false);

        if ((!player.isAtk || AtkTime >= 0.25f) && !isAnim)
        {
            if (Input.GetKey(KeyCode.LeftControl) && !anim.GetBool("isFall") && !isSlide)
            {
                rigid2D.velocity = new Vector2(x * player.status.moveSpeed / 2, rigid2D.velocity.y);
                player.isAtk = false;
                atkNum = 1;
                StopCoroutine("ComboAtk");
            }
            else
                rigid2D.velocity = new Vector2(x * player.status.moveSpeed, rigid2D.velocity.y);
        }
        else
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.LeftArrow) && !isAnim && !isSlide)
        {
            renderer.flipX = true;
            Weapon.transform.eulerAngles = new Vector3(0, 180, 0);
        }
            
        else if (Input.GetKey(KeyCode.RightArrow) && !isAnim && !isSlide)
        {
            renderer.flipX = false;
            Weapon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
            
    }
    
    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isAnim && !anim.GetBool("isFall") && !isSlide)
            AnimSetBool("Crouch", true);
        else
            AnimSetBool("Crouch", false);
    }

    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && x != 0 && !isAnim && !isSlide && !anim.GetBool("isFall") && !anim.GetBool("isCrouch"))
        {
            AnimSetTrigger("Slide");
            player.status.moveSpeed *= 1.25f;
            isSlide = true;
            


            Invoke("SlideOut", 0.5f);
            x = tempX;
        }
        if (isSlide)
            SlideTime += Time.deltaTime;
        if (SlideTime >= 0.2f && player.status.moveSpeed <= 5f)
            player.status.moveSpeed -= 0.5f;
    }

    void SlideOut()
    {
        player.status.moveSpeed *= 0.75f;
        isSlide = false;
        SlideTime = 0;
    }

    private void Update()
    {
        Attack();
        Jump("Update");
        Idle2();
        Slide();
    }

    public void FixedUpdate()
    {
        if (!isSlide)
        {
            x = Input.GetAxisRaw("Horizontal");
            tempX = x;
        }

        Walk();
        Crouch();
        Jump("FixedUpdate");

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
        {
            AnimSetBool("Fall", false);
            AnimSetBool("Jump", false);
        }
            
        else if (raycastHit.collider == null)
        {
            AnimSetBool("Fall", true);
            if (inputJump)
            {
                AnimSetBool("Jump", true);
                inputJump = false;
            }
        }
    }
}
