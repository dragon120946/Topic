using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySeed : MonoBehaviour
{
    public GameObject energyFlower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(energyFlower, transform.position, transform.rotation);
        }
    }
}
