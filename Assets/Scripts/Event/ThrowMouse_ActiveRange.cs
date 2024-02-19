using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMouse_ActiveRange : MonoBehaviour
{
    public ThrowMouse throwMouse;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            throwMouse.canActive = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            throwMouse.canActive = false;
        }
    }
}
