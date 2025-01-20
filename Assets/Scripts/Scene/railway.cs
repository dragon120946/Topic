using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class railway : MonoBehaviour
{
    SurfaceEffector2D surfaceEffector;

    void Start()
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();
    }

    void Update()
    {
        if (GameDb.isIce)
        {
            surfaceEffector.colliderMask = 1 << 3;
        }
        else
        {
            surfaceEffector.colliderMask = 1 << 9;
        }
    }
}
