using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly_ActiveRange : MonoBehaviour
{
    public Butterfly butterfly;
    public S1Mgr s1;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            butterfly.canActive = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            butterfly.canActive = false;
            s1.butterfly = true;
        }
    }
}
