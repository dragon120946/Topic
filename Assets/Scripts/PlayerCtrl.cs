using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class PlayerCtrl : MonoBehaviour
{
    #region public
    public int animHurt;        //狀態機的水滴大小
    public float dashSpeed;     //衝刺速度
    public S1Mgr s1;
    public float timeType;      //型態持續時間

    [Header("Jump")]
    public float jumpForce;     //跳躍高度
    public bool canJump;        //可以跳躍

    [Header("Type")]
    public bool isWater;
    public bool isIce;
    public bool isSteam;
    #endregion

    #region private
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rb;
    private Animator animator;
    
    private bool isTouchVine;       //碰到藤蔓
    private bool isTouchWater1;     //碰到水坑
    private bool isTouchWater2;
    private bool isTouchWater3;
    private bool isHurt;            //受傷

    #endregion
    
    void Start()
    {
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        
        capsuleCollider2D.size = new Vector2(1.1f, 0.9f);
        animHurt = 1;
        isTouchVine = false;
        isTouchWater1 = false;
        isTouchWater2 = false;
        isTouchWater3 = false;
        isHurt = false;
        isWater = true;
    }

    void FixedUpdate()
    {
        if (isWater)
        {
            WaterType();
        }
        if (isIce)
        {
            IceType();
        }
        if (isSteam)
        {
            SteamType();
        }
    }

    void Update()
    {
        GameDb.hp = animHurt;

        if (animHurt > 5)
        {
            animHurt = 5;
        }
        if(capsuleCollider2D.offset.y > 0.06f)
        {
            capsuleCollider2D.offset = new Vector2(0f, 0.06f);
        }

        if (capsuleCollider2D.size.x > 1.82f && capsuleCollider2D.size.y > 1.62f)
        {
            capsuleCollider2D.size = new Vector2(1.82f, 1.62f);
        }
    }

    #region 碰撞觸發
    void OnCollisionEnter2D(Collision2D col)
    {
        //水碰到tag冰變成冰
        if (isWater && col.gameObject.CompareTag("Ice"))
        {
            isIce = true;
            isWater = false;
            isSteam = false;
        }

        //水碰到tag火變蒸氣
        if (isWater && col.gameObject.CompareTag("Fire"))
        {
            isSteam = true;
            isWater = false;
            isIce = false;
        }
        //碰到地板或陷阱可跳躍
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Trap"))
        {
            canJump = true;
            animator.Play("Water1_OnGround");
        }
        //碰到陷阱或敵人就變小
        if (isWater && (col.gameObject.CompareTag("Trap") || col.gameObject.CompareTag("Enemy")))
        {
            StartCoroutine("HurtCD");
            if (isHurt)
            {
                return;
            }
            
            animHurt--;
            capsuleCollider2D.offset -= new Vector2(0f, 0.02f);
            capsuleCollider2D.size -= new Vector2(0.18f, 0.18f);
            animator.SetInteger("HurtTime", animHurt);
            isHurt = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isWater && collision.gameObject.CompareTag("Vine"))     //水型態碰到藤蔓可吸附在上面
        {
            transform.SetParent(collision.gameObject.transform);
            rb.velocity = Vector2.zero;
            isTouchVine = true;
            OnVine();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (isWater && collision.gameObject.CompareTag("Vine"))
        {
            isTouchVine = false;
        }
        if (collision.gameObject)
        {
            canJump = false;
        }

        if (isWater && collision.gameObject.CompareTag("Trap"))
        {
            StopCoroutine("HurtCD");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //水碰到tag冰變成冰
        if (isWater && collision.gameObject.CompareTag("Ice"))
        {
            isIce = true;
            isWater = false;
            isSteam = false;
            timeType = 0;
        }

        //水碰到tag火變蒸氣
        if (isWater && collision.gameObject.CompareTag("Fire"))
        {
            isSteam = true;
            isWater = false;
            isIce = false;
            timeType = 0;
            //animator.SetBool("SteamType", true);
            animator.Play("Steam1_Idle");
        }
        //冰碰火或火碰冰會變水
        if ((isSteam && !collision.gameObject.CompareTag("Fire")) ||
            (isIce && collision.gameObject.CompareTag("Fire")))
        {
            isWater = true;
            isIce = false;
            isSteam = false;
            timeType = 0;
            //animator.SetBool("SteamType", false);
            animator.Play("Water1_Idle");

        }
        
        //水蒸氣碰到爆炸花就死掉
        if (isSteam && collision.gameObject.CompareTag("Explode"))
        {
            animHurt = 0;
        }

        //碰到能量花就加能量
        if (collision.gameObject.CompareTag("Energy"))
        {
            GameDb.energy++;
            Destroy(collision.gameObject);
        }

        #region 碰到水坑
        if (collision.gameObject.name == "WaterHole")
        {
            if (isTouchWater1)
            {
                canJump = true;
                return;
            }
            
            animHurt++;
            animator.SetInteger("HurtTime", animHurt);
            animator.SetBool("JumpingUp", false);
            animator.SetBool("JumpingFall", false);
            
            
            capsuleCollider2D.offset += new Vector2(0f, 0.02f);
            capsuleCollider2D.size += new Vector2(0.18f, 0.18f);
            isTouchWater1 = true;
            canJump = true;
        }
        if (collision.gameObject.name == "WaterHole (1)")
        {
            if (isTouchWater2)
            {
                canJump = true;
                return;
            }

            animHurt++;
            animator.SetInteger("HurtTime", animHurt);
            capsuleCollider2D.offset += new Vector2(0f, 0.02f);
            capsuleCollider2D.size += new Vector2(0.18f, 0.18f);
            isTouchWater2 = true;
            canJump = true;
        }
        if (collision.gameObject.name == "WaterHole (2)")
        {
            if (isTouchWater3)
            {
                canJump = true;
                return;
            }
            
            animHurt++;
            animator.SetInteger("HurtTime", animHurt);
            capsuleCollider2D.offset += new Vector2(0f, 0.02f);
            capsuleCollider2D.size += new Vector2(0.18f, 0.18f);
            isTouchWater3 = true;
            canJump = true;
        }
        #endregion
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            canJump = false;
        }
    }
    #endregion

    #region Type
    public void WaterType() //水型態
    {
        //animator.Play("Water1_Idle");
        
        capsuleCollider2D.isTrigger = false;
        if (isTouchVine)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (s1.slidStamina.value < 10)
                {
                    return;
                }
                s1.slidStamina.value -= 10;
                transform.Translate(transform.right * Time.deltaTime * -dashSpeed);
            }
            transform.position -= new Vector3(0.1f, 0f, 0f);
            //animator.SetBool("LeftWalking", true);
            
            animator.Play("Water1_Walk_Left");

        }
        //如果下面這一段被啓用，移動會陷入"向左移動-IDLE-向左移動-。。。"的循環卡住，再往下按住D的那個也是同理
        /*else
        {

            //animator.SetBool("LeftWalking", false);
            animator.Play("Water1_Idle");
        }*/

        if (Input.GetKey(KeyCode.D))
        {   
            if (Input.GetMouseButtonDown(1))
            {
                if (s1.slidStamina.value < 10)
                {
                    return;
                }
                s1.slidStamina.value -= 10;
                transform.Translate(transform.right * Time.deltaTime * dashSpeed);
            }
            transform.position += new Vector3(0.1f, 0f, 0f);
            //animator.SetBool("RightWalking", true);
            animator.Play("Water1_Walk_Right");
        }
        /*else
        {
            //animator.SetBool("RightWalking", false);
            animator.Play("Water1_Idle");
        }*/

        Jump();
    }

    public void IceType()   //冰型態
    {
        timeType += 1f * Time.deltaTime;
        if (timeType > 10f)
        {
            timeType = 0f;
            isIce = false;
            isWater = true;
        }
        
        rb.gravityScale = 1.5f;
        //移動
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetMouseButtonDown(1))
            {
                transform.Translate(transform.right * Time.deltaTime * -dashSpeed);
            }
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetMouseButtonDown(1))
            {
                transform.Translate(transform.right * Time.deltaTime * dashSpeed);
            }
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
        Jump(); 
    }

    public void SteamType() //蒸氣型態
    {
        timeType += 1f * Time.deltaTime;
        if (timeType > 10f)
        {
            timeType = 0f;
            isIce = false;
            isWater = true;
        }
        
        capsuleCollider2D.isTrigger = true;
        rb.gravityScale = -0.5f;
        //移動
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0.08f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.08f, 0f, 0f);
        }
    }
    #endregion

    public IEnumerator HurtCD()
    {
        for (int i = animHurt; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            animHurt--;
        }
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(transform.up * jumpForce);
        }
        if (rb.velocity.y > 0f)      //力向上時播放向上跳動畫
        {
            //animator.SetBool("JumpingUp", true);
            animator.Play("Water1_Jump");
        }
        
        //原始跳躍會緊接著降落，暴力播放會帶來更多的卡頓問題。
        else
        {
            //animator.SetBool("JumpingUp", false);
            //animator.Play("Water1_Idle");
        }
        if (rb.velocity.y < 0f)      //力向下時播放播放降落動畫
        {
            //animator.SetBool("JumpingFall", true);
            animator.Play("Water1_Fall");

        }
        else
        {
            //animator.SetBool("JumpingFall", false);
            //animator.Play("Water1_Idle");
        }
    }

    void OnVine()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0f, 0.1f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0f, 0.1f, 0f);
        }
    }
}
