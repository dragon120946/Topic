using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2Mgr : GameManager
{
 

    // Start is called before the first frame update
    void Start()
    {
        Base_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Base_Update();
        if (GameDb.hp <= 0)
        {
            Dead();
        }
        if (GameDb.isBossWar)
        {
           //改為Boss戰音樂
        }
    }
}
