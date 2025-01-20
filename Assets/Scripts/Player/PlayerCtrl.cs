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
    public float fallTime = 0;
    public GameObject waterBall;            //水球
    public GameObject fxSteam;              //蒸氣特效
    public GameObject fxHurt;               //受傷特效
    [NonSerialized]
    public Vector2 moveVector;
    public float paralysisTimer;            //麻痺計時器

    [NonSerialized] 
    public bool alreadyPressButton;         //是否按下觸發種子的鍵

    [Header("型態轉換")]
    [NonSerialized]
    public float typeTimer;                 //型態計時器
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

    [NonSerialized]
    public Animator animator;
    #endregion

    #region private
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private bool isTouchVine;               //碰到藤蔓
    private bool isparalysis;               //是否麻痺
    [SerializeField]
    private float fallTimer;
    #endregion
    
    void Start()
    {
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        transform.localScale = new Vector3(1f, 1f, 1f);
        originSpeed = moveSpeed;
        isparalysis = false;
        isTouchVine = false;
        fxHurt.SetActive(false);
        fxSteam.SetActive(false);
        GameDb.isWater = true;
        
        if(GameDb.level <= 1 && !GameDb.isSave)
        {
            GameDb.hp = 20;
            GameDb.energy = 0;
        }

        if (GameDb.hp > 100)
        {
            GameDb.hp = 100;
        }
        if (GameDb.hp > 80 && GameDb.hp <= 100)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }
        if (GameDb.hp > 60 && GameDb.hp <= 80)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        }
        if (GameDb.hp > 40 && GameDb.hp <= 60)
        {
            transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }
        if (GameDb.hp > 20 && GameDb.hp <= 40)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        if (GameDb.hp <= 20)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
            //animator.SetTrigger("TurnSteam");

        }
        if (rb.velocity.y != 0)
        {

            if (rb.velocity.y < -3f && GameDb.isWater)      //力向下時播放降落動畫
            {
                
                rb.velocity += Vector2.down * 0.1f;
                fallTime += Time.deltaTime;
                if (fallTime > 0.2f)
                {
                    fallTime = 0f;
                    animator.SetBool("Falling", true);

                }

            }
            else
            {
                fallTime = 0;
            }
            
        }
        else
        {
            animator.SetBool("Falling", false);
        }
       
        //墜落速度不會大於15
        if (rb.velocity.y <= -15)
        {
            rb.velocity = new Vector2(rb.velocity.x, -15f);
        }
    }

    void Update()
    {
        if (GameDb.hp > 100)
        {
            GameDb.hp = 100;
        }
        if (GameDb.hp > 80 && GameDb.hp <= 100)
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

        if (isparalysis)
        {
            paralysisTimer += Time.deltaTime;
            if (paralysisTimer > 1.5f)
            {
                isparalysis = false;
                paralysisTimer = 0;
            }
        }
    }

    #region 碰撞觸發

    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        //水碰到tag冰變成冰
        if (GameDb.isWater && col.gameObject.CompareTag("Ice"))
        {
            GameDb.isIce = true;
            GameDb.isWater = false;
            GameDb.isSteam = false;
            audiosource.clip = iceAudio[0];
            audiosource.Play();
        }
        */
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
            animator.SetBool("Falling", false);
            GameDb.isGround = true;
        }
        //碰到電水母左右相反1.5秒
        if (col.gameObject.name == "ElecJelly")
        {
            isparalysis = true;
        }

        if (col.gameObject)
        {
            rb.velocity = Vector2.zero;
        }
    }
    /*
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
*/
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
            typeTimer = iceTime;
            audiosource.clip = iceAudio[0];
            audiosource.Play();
            GameDb.touchIce = true;
            animator.SetTrigger("TurnIce");
        }
        //冰再碰到tag冰，倒數重置
        if (GameDb.isIce && collision.gameObject.CompareTag("Ice"))
        {
            typeTimer = iceTime;
        }
        //冰碰到火牆，減持續時間
        if (GameDb.isIce && collision.gameObject.name == "Circle")
        {
            typeTimer -= 2f;
        }
        //水碰到tag火變蒸氣
        if (GameDb.isWater && collision.gameObject.CompareTag("Fire"))
        {
            GameDb.isSteam = true;
            GameDb.isWater = false;
            GameDb.isIce = false;
            typeTimer = steamTime;
            audiosource.clip = steamAudio[0];
            audiosource.Play();
            fxSteam.SetActive(true);
            fxSteam.GetComponent<ParticleSystem>().Play();
            rb.AddForce(150f * Vector2.up);
        }
        //蒸氣再碰到tag火，倒數重置
        if (GameDb.isSteam && collision.gameObject.CompareTag("Fire"))
        {
            typeTimer = steamTime;
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
            typeTimer = 0;
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
        circleCollider2D.isTrigger = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        animator.SetBool("IceType", true);
        typeTimer -= Time.deltaTime;
        if (typeTimer <= 0f)     //如果時間超過10秒就變回水滴
        {
            typeTimer = iceTime;
            GameDb.isIce = false;
            GameDb.isWater = true;

            audiosource.clip = iceAudio[1];
            audiosource.Play();
            animator.SetBool("IceType", false);
        } 
        /*
        if (!Input.GetButton("Horizontal"))
        {
            audiosourceLoop.clip = iceAudio[2];
            audiosourceLoop.Play();
        }
        */
        rb.gravityScale = 2f;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void SteamType() //蒸氣型態
    {
        typeTimer -= Time.deltaTime;
        if (typeTimer <= 0f)
        {
            typeTimer = steamTime;
            GameDb.isSteam = false;
            GameDb.isWater = true;
            animator.SetBool("SteamType", false);
            audiosource.clip = steamAudio[1];
            audiosource.Play();
        }
        
        if (!Input.GetButton("Horizontal"))
        {
            audiosourceLoop.clip = steamAudio[2];
            audiosourceLoop.Play();
        }

        circleCollider2D.isTrigger = true;
        rb.gravityScale = -0.5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity += new Vector2(0f, 0.15f);
        //飛行速度不超過10
        if (rb.velocity.y >= 10)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    #endregion

    #region 行為
/*
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
*/
    public void Damage(int value)
    {
        GameDb.hp -= value;
        Debug.Log("I Hurt 傷害 = " + value + " 剩餘血量 = " + GameDb.hp);
        if (GameDb.hp <= 0)
        {
            //animator.Play("Water_Die");
            audiosource.clip = waterAudio[4];
            audiosource.Play();
        }
        else
        {
            //animator.Play("Water_Hurt");
            audiosource.clip = waterAudio[3];
            audiosource.Play();
        }
        if (GameDb.isWater)
        {
            animator.SetTrigger("Hurt");
        }
        fxHurt.SetActive(true);
        fxHurt.GetComponent<ParticleSystem>().Play();
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (isparalysis)
        {
            moveVector = -context.ReadValue<Vector2>();
        }
        else
        {
            moveVector = context.ReadValue<Vector2>();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameDb.isGround && GameDb.isWater)
            {
                animator.SetTrigger("Jump");

                rb.AddForce(transform.up * jumpForce);
                audiosource.clip = waterAudio[0];
                audiosource.Play();
                GameDb.isGround = false;
            }
        }
    }

    public void TouchSeed(InputAction.CallbackContext context)
    {
        //started
        //performed
        //cancel
        if (context.performed)
        {
            alreadyPressButton = true;
        }
        else
        {
            alreadyPressButton = false;
        }
    }

    #endregion
}
