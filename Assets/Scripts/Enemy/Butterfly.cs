using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : EnemyBehavior
{
    public float speed;
    public float amp;   //震幅
    public float fre;   //頻率
    public GameObject activeRange;
    public Transform originPoint;
    public GameObject player;
    [NonSerialized]
    public bool canActive = false;

    void Start()
    {
        
    }

    void Update()
    {
        //面向玩家
        if (player.transform.position.x > this.gameObject.transform.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        //跟隨玩家
        if (canActive)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originPoint.transform.position, speed * Time.deltaTime);
        }

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
