using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using PathCreation;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBehavior
{
    public GameObject bossState;    //Boss血量
    public GameObject fireFishs;    //火金魚
    public GameObject fireBall;     //火球
    public GameObject exploFlower;  //追蹤火球
    public GameObject player;
    public GameObject firePillars;  //火柱
    public GameObject chip;         //寶珠碎片
    public Transform chipPoint;     //碎片位置
    public int flowerMaxCount;      //爆炸花的最大數量
    public int maxHP;               //最大血量
    public AnimationCurve 散射;     //射擊角度曲線圖(大概)
    public float 計算散射時間;
    public float 發射間隔;
    public List<GameObject> bossHPList;

    [Header("Path")]

    public PathCreator path;
    public EndOfPathInstruction endEvent;
    public float pathProcess;

    [NonSerialized]
    public bool canFarAttack;       //可以遠距攻擊

    bool canHurt;
    // Start is called before the first frame update
    void Start()
    {
        GameDb.isBossWar = false;
        canFarAttack = false;
        canHurt = true;
        firePillars.SetActive(false);
        bossState.SetActive(false);
        hp = maxHP;
        //hp = 20;
    }

    public void FixedUpdate()
    {
        if (GameDb.isIce)
        {
            fireFishs.SetActive(false);
            gameObject.tag = "Untagged";
        }
        else
        {
            fireFishs.SetActive(true);
            gameObject.tag = "Explode";
            
        }
        if (GameDb.isBossWar)
        {
            Stage1();
        }
        
        if (transform.position == path.path.GetPoint(21) ||
            (GameDb.isBossWar && player.transform.position.x < transform.position.x))
        {
            GameDb.hp = 0;
        }
        
    }

    void Update()
    {
        //死亡時
        if(hp <= 0)
        {
            Dead();
        }
    }

    void Stage1()
    {
        //血量小於一半時進入二階段
        if (hp <= maxHP / 2)
        {
            Stage2();
            return;
        }
        Move();
        FarAttack();
        /*
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
        */
    }
    void Stage2()
    {
        Move();
        FarAttack();
        firePillars.SetActive(true);
        gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }

    void Move()
    {
        pathProcess += Time.deltaTime * 3;
        transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
    }
    /*
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
    */
    void FarAttack()
    {
        發射間隔 += Time.deltaTime;
  
        if (發射間隔 >= 3f)
        {
            發射間隔 = 0f;
            GameObject flower = Instantiate(exploFlower, transform.position +
                new Vector3(6, -1, 0), Quaternion.identity);

            flower.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
    }

    void Dead()
    {
        GameDb.isBossWar = false;
        Instantiate(chip, chipPoint.position, Quaternion.identity);
        Destroy(bossState);
        Destroy(gameObject);
        Destroy(firePillars);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        //碰到非冰的玩家，玩家受到傷害
        if (collision.gameObject.CompareTag("Player") && !GameDb.isIce)
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Damage(attack);
        }
        //碰到冰的玩家扣20血
        if (collision.gameObject.CompareTag("Player") && GameDb.isIce)
        {
            this.enabled = false;
            if (canHurt)
            {
                hp -= 20;
            }
            canHurt = false;
            受傷要做的事();
            
        }
        //被礦車撞到時進入BOSS戰
        if (collision.gameObject.name == "Minecart")
        {
            GameDb.isBossWar = true;
            Destroy(collision.gameObject);
            bossState.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "lava")
        {
            pathProcess = 0;
            canHurt = true;
            this.enabled = true;
        }
    }

    public void 受傷要做的事()
    {
        
        //hp除20於餘數，hp除20等於整數
        //如果餘數是20，大小為1
        int 餘數 = hp % 20;
        int 整數 = hp / 20;
        
        Debug.Log(餘數);
        {
            switch(餘數)
            {
                case 20 :
                bossHPList[整數].transform.localScale = new Vector3(1,1,1);
                    break;
                case 15 :
                bossHPList[整數].transform.localScale = new Vector3(0.75f,0.75f,1);
                    break;
                case 10 :
                bossHPList[整數].transform.localScale = new Vector3(0.5f,0.5f,1);
                    break;
                case 5 :
                bossHPList[整數].transform.localScale = new Vector3(0.25f,0.25f,1);
                    break;
                case 0 :
                bossHPList[整數].transform.localScale = new Vector3(0,0,1);
                    break;
            }
        }
    }
}
