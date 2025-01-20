using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirePillars : MonoBehaviour
{
    //[SerializeField] List<GameObject> fireList = new List<GameObject>();
    [SerializeField] GameObject fire;
    [SerializeField] ParticleSystem vfxFire;
    [SerializeField] float maxTime;
    [SerializeField] float timer;
    //[SerializeField] int ballCount;
    //[SerializeField] bool increase;

    // Start is called before the first frame update
    void Start()
    {
        vfxFire.Play();
        /*
        for (int i = 0; i < 4; i++)
        {
            fireList[i].SetActive(false);
        }
        ballCount = 0;
        increase = true;
        */
    }

    // Update is called once per frame
    void Update()
    {
        fire.gameObject.transform.position += new Vector3(0, 0.013f, 0);
   

        if (vfxFire.isStopped)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                timer = 0;
                vfxFire.Play();
                fire.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.13f, transform.position.z);
                /*
                if (ballCount < fireList.Count && increase)
                {

                    fireList[ballCount].SetActive(true);
                    ballCount++;
                    timer = 0;
                    if (ballCount == fireList.Count)
                    {
                        increase = false;
                        ballCount--;
                    }
                }
                else if (increase == false && ballCount > -1)
                {
                    //this.enabled = false;
                    fireList[ballCount].SetActive(false);
                    ballCount--;
                    timer = 0;
                    if (ballCount == -1)
                    {
                        increase = true;
                        ballCount++;
                    }
                }
                */
            }
           
        }
        
       
        
    }
}
