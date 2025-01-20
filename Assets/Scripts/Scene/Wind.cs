using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float timer;
    private AreaEffector2D wind;
    // Start is called before the first frame update
    void Start()
    {
        wind = gameObject.GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 4.5f)
        {
            timer = 0;
        }

        if(timer >= 0 && timer < 1.5f)
        {
            wind.forceMagnitude = -2;
        }
        if (timer >= 1.5f && timer < 3)
        {
            wind.forceMagnitude = -4;
        }
        if (timer >= 3 && timer < 4.5f)
        {
            wind.forceMagnitude = -6;
        }
    }
}
