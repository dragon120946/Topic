using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToFire : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameDb.isGetKey)
        {
            SceneManager.LoadScene("Loading");
            GameDb.level++;
        }
    }
}
