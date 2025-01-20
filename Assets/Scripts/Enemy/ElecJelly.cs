using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecJelly : EnemyBehavior
{
    public Transform up;
    public Transform down;
    public float speed;
    public float moveInterval;

    private Animator animator;
    private Rigidbody2D rb;
    private bool walkUp = true; //是否往上走

    void Start()
    {
        this.transform.DetachChildren();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>(); //抓元件使用
    }

    void Update()
    {
        Enemy_Update();
        // 判斷
        if (up.localPosition.y < this.transform.localPosition.y)
        {
            walkUp = false;
        }
        if (down.localPosition.y > this.transform.localPosition.y)
        {
            walkUp = true;
        }
        //執行
        if (walkUp)
        {
            rb.velocity = new Vector2(0, Mathf.PingPong(moveInterval * Time.time, speed));

        }
        else
        {
            rb.velocity = new Vector2(0, -Mathf.PingPong(moveInterval * Time.time, speed));

        }
    }
}
