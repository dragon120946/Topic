using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train_Target : MonoBehaviour
{
    public TrainMgr train;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);
            train.isTargetDestroy = true;
        }
    }
}
