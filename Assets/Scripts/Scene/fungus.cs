using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fungus : MonoBehaviour
{
    public float push;
    private AudioSource audio; 
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
       // Debug.Log("c");
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("a");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(push  *Vector2.up);
            GameDb.isGround = false;
        }
        audio.Play();
    }
}