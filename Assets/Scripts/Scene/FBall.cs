using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameDb.isIce)
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Damage(10);
        }
    }
}
