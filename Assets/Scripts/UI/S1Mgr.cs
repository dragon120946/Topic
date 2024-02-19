using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S1Mgr : GameManager
{

    public void Start()
    {
        Base_Start();
    }

    public void Update()
    {
        Base_Update();
        if (GameDb.hp <= 0)
        {
            Dead();
        }
    }
}
