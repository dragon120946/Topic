using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_Wall : MonoBehaviour
{
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameDb.index == 27)
        {
            GameDb.level++;
            SceneManager.LoadScene("Loading");
        }
    }
}
