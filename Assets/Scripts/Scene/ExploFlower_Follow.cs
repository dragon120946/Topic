using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploFlower_Follow : MonoBehaviour
{
    public Boss boss;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.right * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Damage(10);
            //boss.i--;
            Destroy(gameObject);
        }
        if (collision.gameObject)
        {
            //boss.i--;
            Destroy(gameObject);
        }
    }
}
