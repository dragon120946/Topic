using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    public float maxTime;
    private float timer;
    private bool allowHurt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxTime && allowHurt == false)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                timer = 0;
                allowHurt = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (GameDb.isWater && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("aaa");
            other.gameObject.GetComponent<PlayerCtrl>().moveSpeed = other.gameObject.GetComponent<PlayerCtrl>().originSpeed / 2;
            if (allowHurt == true)
            {
                Debug.Log("bbb");
                allowHurt = false;
                other.gameObject.GetComponent<PlayerCtrl>().Damage(10);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (GameDb.isWater && other.gameObject.CompareTag("Player"))
        { 
            other.gameObject.GetComponent<PlayerCtrl>().moveSpeed = other.gameObject.GetComponent<PlayerCtrl>().originSpeed;
        }
    }
}
