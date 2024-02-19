using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train_Seed : MonoBehaviour
{
    public GameObject imgF;
    public GameObject leaf;
    public TrainMgr train;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            imgF.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerCtrl>().alreadyPressButton)
        {
            train.touchSeed = true;
            Destroy(gameObject);
            leaf.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            imgF.gameObject.SetActive(false);
        }
    }
}
