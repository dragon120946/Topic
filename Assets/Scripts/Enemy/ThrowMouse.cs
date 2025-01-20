using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowMouse : MonoBehaviour
{
    //設置“已經丟過種子了=否”，即還沒丟過種子
    public bool hasThrownSeed = false;
    //發射種子的位置
    public Transform shootPosition;
    //種子，它可以被發射
    public GameObject energySeed;

    public PathCreator path;
    public EndOfPathInstruction endEvent;
    public GameObject activeRange;
    public float pathProcess;
    public float pathSpeed = 10;

    //public float maxTime;
    [NonSerialized]
    public bool canActive = false;
    
    //private bool aniActive = false;
    //private bool aniSeed = false;
    private float timer;
    
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {

        bool hasThrownSeed = false;
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (canActive)
        {
            //松鼠移動
            if (pathProcess < 0)
            {
                pathProcess = 0;
            }
            pathProcess += Time.deltaTime * pathSpeed;
            transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
            
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("run", true);
            
            //if (transform.position == path.path.GetPoint(80) && !hasThrownSeed )
            if (transform.position == path.path.GetPoint(80))
            {
                animator.SetBool("run", false);
                //animator.SetBool("preparing",true);
                if (!hasThrownSeed)
                {
                    animator.SetBool("preparing",true);
                }
            }

        }
        else
        {
            //松鼠離開
            if (pathProcess > 13)
            {
                pathProcess = 13;
            }
            pathProcess -= Time.deltaTime * pathSpeed;
            transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
            
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("run", true);
            //animator.SetBool("preparing",false);

            if (transform.position == path.path.GetPoint(0))
            {
                animator.SetBool("run", false);
                //animator.SetBool("preparing",true);

            }
        }
        
    }

    
    void Attack()
    {
        if (!hasThrownSeed )
        {    
            hasThrownSeed = true;
            animator.SetTrigger("attacking");
            animator.SetBool("preparing",false);
            //ExecuteAfterDelay();
            Invoke("Shoot",.8f);
        }
    }
    void Shoot()
    {
        GameObject fire = Instantiate(energySeed, shootPosition.position, shootPosition.rotation);
        fire.GetComponent<Rigidbody2D>().velocity = shootPosition.right * 19f;
        
    }
    
}
