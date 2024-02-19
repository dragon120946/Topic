using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerCtrl : MonoBehaviour
{
    #region public

    public GameObject waterBall;            //水球
    public GameObject fxSteam;              //蒸氣特效
    [NonReorderable] 
    public bool alreadyPressButton;         //是否按下觸發種子的鍵

    [Header("型態轉換")]
    [NonSerialized]
    public float timer;                     //型態持續時間
    public float steamTime;                 //變成蒸氣的時間
    public float iceTime;                   //變成冰的時間

    [Header("Move")]
    public float moveSpeed;
    [NonSerialized]
    public float originSpeed;               //原始速度
    public float dashSpeed;                 //衝刺速度

    [Header("Jump")]
    public float jumpForce;                 //跳躍高度

    [Header(("Audio"))]
    public AudioSource audiosource;
    public AudioSource audiosourceLoop;
    public List<AudioClip> waterAudio;
    public List<AudioClip> iceAudio;
    public List<AudioClip> steamAudio;

    #endregion

    #region private
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    private bool isTouchVine;               //碰到藤蔓
    private Vector2 moveVector;
    [SerializeField]
    private float fallTimer;
    #endregion
    
    void Start()
    {
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        transform.localScale = new Vector3(1f, 1f, 1f);
        originSpeed = moveSpeed;
        isTouchVine = false;
        fxSteam.SetActive(false);
        GameDb.isWater = true;
        if(GameDb.level <= 1)
        {
            GameDb.hp = 20;
            GameDb.energy = 0;
        }
    }

    void FixedUpdate()
    {
        if (GameDb.isWater)
        {
            WaterType();
            //水移動
            if (moveVector != Vector2.zero)
            {
                transform.position += new Vector3(moveVector.x * moveSpeed * Time.deltaTime, 0f, 0f);
                animator.SetBool("Walking", true);
                animator.SetFloat("Dir", moveVector.x);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            animator.SetBool("SteamType", false);
            animator.SetBool("IceType", false);
        }
        if (GameDb.isIce)
        {
            IceType();
            //冰移動
            if (moveVector != Vector2.zero)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (GameDb.stamina < 10)
                    {
                        return;
                    }
                    GameDb.stamina -= 10;
                    rb.velocity = new Vector2(moveVector.x * moveSpeed * 1.2f * 1.5f, rb.velocity.y);
                }
                rb.velocity = new Vector2(moveVector.x * moveSpeed * 1.2f, rb.velocity.y);
            }

            animator.SetBool("IceType", true);
        }
        if (GameDb.isSteam)
        {
            SteamType();
            //蒸氣移動
            if (moveVector != Vector2.zero)
            {
                transform.position += new Vector3(moveVector.x * moveSpeed * Time.deltaTime, 0f, 0f);
            }

            animator.SetBool("SteamType", true);
        }
        if(rb.velocity.y != 0)
        {
            if (rb.velocity.y > 1f)              //力向上時播放跳躍動畫
            {  
                
                animator.SetBool("Jumping", true);
                animator.SetFloat("High", 1);
            }
            else if (rb.velocity.y < -1f)      //力向下時播放降落動畫
            {
                rb.velocity += Vector2.down * 0.1f;
                if (fallTimer >= 0.25f)
                {
                    fallTimer = 0;
                    
                    animator.SetBool("Jumping", true);
                    animator.SetFloat("High", -1);
                }
                else
                {
                    animator.SetFloat("High", 0);
                    if (GameDb.isGround == true)
                    {
                        fallTimer = 0;
                    }
                }
            }
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
        /*
        else if (rb.velocity.y > -0.4f && rb.velocity.y < 0f)
        {
            animator.SetBool("Jumping", true);
        }
        */
        //墜落速度不會大於15
        if (rb.velocity.y <= -15)
        {
            rb.velocity = new Vector2(rb.velocity.x, -15f);
        }
    }

    void Update()
    {
        if (GameDb.hp == 100)
        {
            GameDb.hp = 100;
        }
        if(GameDb.hp > 80 && GameDb.hp <= 100)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }
        if(GameDb.hp > 60 && GameDb.hp <= 80)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        }
        if(GameDb.hp > 40 && GameDb.hp <= 60)
        {
            transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }
        if(GameDb.hp > 20 && GameDb.hp <= 40)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        if(GameDb.hp <= 20)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    #region 碰撞觸發

    void OnCollisionEnter2D(Collision2D col)
    {
        //水碰到tag冰變成冰
        if (GameDb.isWater && col.gameObject.CompareTag("Ice"))
        {
            GameDb.isIce = true;
            GameDb.isWater = false;
            GameDb.isSteam = false;
            audiosource.clip = iceAudio[0];
            audiosource.Play();
        }

        //水碰到tag火變蒸氣
        if (GameDb.isWater && col.gameObject.CompareTag("Fire"))
        {
            GameDb.isSteam = true;
            GameDb.isWater = false;
            GameDb.isIce = false;
            audiosource.clip = steamAudio[0];
            audiosource.Play(); 
        }
        
        //碰到地板或陷阱可跳躍
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Trap"))
        {
            GameDb.isGround = true;
        }

        if (col.gameObject)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //水型態碰到藤蔓可吸附在上面
        if (GameDb.isWater && collision.gameObject.CompareTag("Vine"))     
        {
            transform.SetParent(collision.gameObject.transform);
            rb.velocity = Vector2.zero;
            isTouchVine = true;
            OnVine();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (GameDb.isWater && collision.gameObject.CompareTag("Vine"))
        {
            isTouchVine = false;
        }

        if (GameDb.isWater && collision.gameObject.CompareTag("Trap"))
        {
            GameDb.touchSand = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //水碰到tag冰變成冰
        if (GameDb.isWater && collision.gameObject.CompareTag("Ice"))
        {
            GameDb.isIce = true;
            GameDb.isWater = false;
            GameDb.isSteam = false;
            timer = iceTime;
            audiosource.clip = iceAudio[0];
            audiosource.Play();
            GameDb.touchIce = true;
        }

        //水碰到tag火變蒸氣
        if (GameDb.isWater && collision.gameObject.CompareTag("Fire"))
        {
            GameDb.isSteam = true;
            GameDb.isWater = false;
            GameDb.isIce = false;
            timer = steamTime;
            audiosource.clip = steamAudio[0];
            audiosource.Play();
            fxSteam.SetActive(true);
            fxSteam.GetComponent<ParticleSystem>().Play();
            rb.AddForce(150f * Vector2.up);
        }
        //蒸氣再碰到tag火，倒數重置
        if (GameDb.isSteam && collision.gameObject.CompareTag("Fire"))
        {
            timer = steamTime;
        }

        //冰碰火或火碰冰會變水
        if ((GameDb.isSteam && !collision.gameObject.CompareTag("Fire")) ||
            (GameDb.isIce && collision.gameObject.CompareTag("Fire")))
        {
            if (GameDb.isSteam && !collision.gameObject.CompareTag("Fire"))
            {
                audiosource.clip = steamAudio[1];
                audiosource.Play();
            }
            if (GameDb.isIce && collision.gameObject.CompareTag("Fire"))
            {
                audiosource.clip = iceAudio[1];
                audiosource.Play();
            }
            GameDb.isWater = true;
            GameDb.isIce = false;
            GameDb.isSteam = false;
            timer = 0;
        }
        
        //碰到爆炸花就死掉
        if (collision.gameObject.CompareTag("Explode"))
        {
            GameDb.hp = 0;
            audiosource.clip = waterAudio[4];
            audiosource.Play();
        }
    }

    #endregion

    #region Type

    public void WaterType() //水型態
    { 
        capsuleCollider2D.isTrigger = false;
        if (isTouchVine)
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
        }    
        if (!Input.GetButton("Horizontal"))
        {
            audiosourceLoop.clip = waterAudio[1];
            audiosourceLoop.Play();
        }
    }

    public void IceType()   //冰型態
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)     //如果時間超過10秒就變回水滴
        {
            timer = iceTime;
            GameDb.isIce = false;
            GameDb.isWater = true;
            animator.SetBool("IceType", false);
            audiosource.clip = iceAudio[1];
            audiosource.Play();
        } 
        /*
        if (!Input.GetButton("Horizontal"))
        {
            audiosourceLoop.clip = iceAudio[2];
            audiosourceLoop.Play();
        }
        */
        rb.gravityScale = 2f;
    }

    public void SteamType() //蒸氣型態
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = steamTime;
            GameDb.isSteam = false;
            GameDb.isWater = true;
            animator.SetBool("SteamType", false);
            audiosource.clip = steamAudio[1];
            audiosource.Play();
        }
        
        if (!Input.GetButton("Horizontal"))
        {
            audiosourceLoop.clip = iceAudio[2];
            audiosourceLoop.Play();
        }

        capsuleCollider2D.isTrigger = true;
        rb.gravityScale = -0.5f;
        rb.velocity += new Vector2(0f, 0.15f);
        //飛行速度不超過10
        if (rb.velocity.y >= 10)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    #endregion

    #region 行為

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

    public void Damage(int value)
    {
        GameDb.hp -= value;
        Debug.Log("I Hurt 傷害 = " + value + " 剩餘血量 = " + GameDb.hp);
        if (GameDb.hp <= 0)
        {
            animator.Play("Water_Die");
            audiosource.clip = waterAudio[4];
            audiosource.Play();
        }
        else
        {
            animator.Play("Water_Hurt");
            audiosource.clip = waterAudio[3];
            audiosource.Play();
        }
    }

    #endregion

    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameDb.isGround)
            {
                rb.AddForce(transform.up * jumpForce);
                audiosource.clip = waterAudio[0];
                audiosource.Play();
                GameDb.isGround = false;
            }
        }
    }

    public void TouchSeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            alreadyPressButton = true;
        }
        else
        {
            alreadyPressButton = false;
        }
    }
}
