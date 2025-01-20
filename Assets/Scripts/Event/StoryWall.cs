using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryWall : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameDb.level == 1 && GameDb.index >= 10)
        {
            boxCollider2D.isTrigger = true;
        }
    }
}
