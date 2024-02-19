using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class ThrowMouse : MonoBehaviour
{
    public PathCreator path;
    public EndOfPathInstruction endEvent;
    public GameObject activeRange;
    public GameObject energySeed;
    public Transform shootPosition;
    public float pathProcess;
    public float maxTime;
    [NonSerialized]
    public bool canActive = false;
    
    private bool aniActive = false;
    private bool aniSeed = false;
    private float timer;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
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
            pathProcess += Time.deltaTime * 5;
            transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Active", true);
            aniActive = true;
            if (transform.position == path.path.GetPoint(80))
            {
                animator.SetBool("UseSeed", true);
                aniSeed = true;
            }
            else
            {
                animator.SetBool("UseSeed", false);
                aniSeed = false;
            }

            if (aniActive && aniSeed)
            {
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    timer = 0;
                    GameObject fire = Instantiate(energySeed, transform.position, transform.rotation);
                    fire.GetComponent<Rigidbody2D>().velocity = shootPosition.right * 15f;
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
            pathProcess -= Time.deltaTime * 5;
            transform.position = path.path.GetPointAtDistance(pathProcess, endEvent);
            
            if(transform.position == path.path.GetPoint(0))
            {
                animator.SetBool("Active", false);
                aniActive = false;
            }
            else
            {
                animator.SetBool("Active", true);
                aniActive = true;
                animator.SetBool("UseSeed", false);
                aniSeed = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }
}
