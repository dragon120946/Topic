using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject leaf;
    //public GameObject vine;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerCtrl>().alreadyPressButton)
        {
            Destroy(gameObject);
            Instantiate(leaf, transform.position + new Vector3(6f, 9f, 0), transform.rotation);
        }
        /*
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);
            Instantiate(vine, transform.position, transform.rotation);
            Debug.Log("Vine");
        }
        */
    }
}
