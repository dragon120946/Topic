using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject leaf;
    public GameObject vine;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && Input.GetMouseButtonDown(0))
        {
            if (gameObject.CompareTag("vine"))
            {
                Instantiate(vine, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
