using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFish : MonoBehaviour
{
    public Transform left;
    public Transform right;

    private Animator animator;
    private Rigidbody2D rb;
 
    private bool walkLeft = true; //是否往左走
 
    void Start()
    {
        this.transform.DetachChildren();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>(); //抓元件使用
    }
 
    void Update()
    {
        // 判斷
        if (left.localPosition.x > this.transform.localPosition.x)
        {
            //Debug.Log("超出左邊界!!");
            animator.SetBool("TurnL",true);
            walkLeft = false;
            

        }
        else if (right.localPosition.x < this.transform.localPosition.x)
        {
            // Debug.Log("超出右邊界!!");
            animator.SetBool("TurnR",true);
            walkLeft = true;

        }
        else
        {
            animator.SetBool("TurnR",false);
            animator.SetBool("TurnL",false);
        }
        //執行
        if (walkLeft)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
