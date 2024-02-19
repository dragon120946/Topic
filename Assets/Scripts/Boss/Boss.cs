using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using PathCreation;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBehavior
{
   
    public GameObject bossHP;       //Boss血量
    public GameObject fireFishs;    //火金魚
    public GameObject fireBall;     //火球
    public GameObject exploFlower;  //跟隨型爆炸花
    public GameObject player;
    public int flowerMaxCount;      //爆炸花的最大數量
    public AnimationCurve 散射;     //射擊角度曲線圖(大概)
    public float 計算散射時間;
    public float 發射間隔;
    public List<GameObject> exploList = new List<GameObject>();

    [Header("Path")]

    public PathCreator path;
    public EndOfPathInstruction endEvent;
    public float pathProcess;

    //[NonSerialized]
    public int i;                   //for迴圈的i,給爆炸花程式用
    [NonSerialized]
    public bool canFarAttack;       //可以遠距攻擊

    private bool alreadyTouchMinecart;
    private bool isStage2;          //進入二階段
    // Start is called before the first frame update
    void Start()
    {
        GameDb.isBossWar = false;
        canFarAttack = false;
        alreadyTouchMinecart = false;
        isStage2 = false;
        fireFishs.SetActive(false);
        bossHP.SetActive(false);
        bossHP.GetComponent<Slider>().value = hp;
    }

    void Update()
    {
        bossHP.GetComponent<Slider>().value = hp;
        
        //血量小於一半時進入二階
        if(hp <= 70)
        {
            Stage2();
        }
        //死亡時
        if(hp <= 0)
        {
            Dead();
        }
    }

    public void FixedUpdate()
    {
        if(alreadyTouchMinecart)
        {
            Stage1();
        }
       
    }

    void Stage1()
    {
        pathProcess += Time.deltaTime * 2f;
        transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
       
        if (!canFarAttack)
        {
            計算散射時間 += Time.fixedDeltaTime;
            if (計算散射時間 <= 10)
            {
                計算散射時間 += Time.deltaTime;
            }
            else if (計算散射時間 > 10)
            {
                計算散射時間 = 10;
            }

            發射間隔 += Time.deltaTime;
            if (發射間隔 >= 0.5f)
            {
                發射間隔 = 0.5f;
            }

            if (發射間隔 == 0.5f)
            {
                發射間隔 = 0;
                NearAttack();
            }
        }
        else
        {
            FarAttack();
        }
    }

    void NearAttack()
    {
        float newAngle = 散射.Evaluate(計算散射時間);
        if (計算散射時間 >= 10)
        {
            計算散射時間 = 0;
        }
        Debug.Log("偏移角度 = " + newAngle );
        GameObject ball = Instantiate(fireBall, transform.position + new Vector3(6, -1, 0), Quaternion.identity);
        ball.transform.eulerAngles = new Vector3(0, 0, newAngle);
    }
    void FarAttack()
    {
        發射間隔 += Time.deltaTime;
        for (i = 0; i < flowerMaxCount; i++)
        {
            if (發射間隔 >= 2f)
            {
                發射間隔 = 0f;
                GameObject flower = Instantiate(exploFlower, transform.position + new Vector3(6, -1, 0), Quaternion.identity);
                flower.transform.position = Vector3.Lerp(flower.transform.position, player.transform.position, 
                    exploFlower.GetComponent<ExploFlower_Follow>().speed * Time.deltaTime);
                //exploList.Add(flower);
                //i = exploList.Count();
            }
        }

    }

    void Stage2()
    {
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    void Dead()
    {
        GameDb.isBossWar = false;
        Destroy(bossHP);
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到玩家，玩家受到傷害
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Damage(attack);
        }
        //被水球打到扣5血
        if (collision.gameObject.CompareTag("Attack") && collision.gameObject.name == "WaterBall(Clone)")
        {
            hp -= 5;
        }
        //被礦車撞到時進入BOSS戰
        if (collision.gameObject.name == "Minecart")
        {
            GameDb.isBossWar = true;
            alreadyTouchMinecart = true;
            Destroy(collision.gameObject);
            bossHP.SetActive(true);
            fireFishs.SetActive(true);
        }
    }
}
