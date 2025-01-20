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
    private bool walkUp = true; //�O�_���W��

    void Start()
    {
        this.transform.DetachChildren();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>(); //�줸��ϥ�
    }

    void Update()
    {
        Enemy_Update();
        // �P�_
        if (up.localPosition.y < this.transform.localPosition.y)
        {
            walkUp = false;
        }
        if (down.localPosition.y > this.transform.localPosition.y)
        {
            walkUp = true;
        }
        //����
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
