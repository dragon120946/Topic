using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploFlower_Follow : MonoBehaviour
{
    public float timer;
    public float speed;
    public float trackTimer;
    public float trackRange;
    public Transform targetTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        if (trackTimer > 0)
        {
            if (targetTrans)
            {
                var diff = targetTrans.position - transform.position;
                transform.rotation = Quaternion.Euler(0, 0,
                    (Mathf.Atan2(diff.y, diff.x) * 180 / Mathf.PI));
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    float d = 45 * i;
                    float x = Mathf.Cos((Mathf.PI / 180) * d);
                    float y = Mathf.Sin((Mathf.PI / 180) * d);
                    var hit = Physics2D.Raycast(transform.position, new Vector2(x, y), trackRange, 1 << 3);
                    Debug.DrawLine(transform.position, transform.position + new Vector3(x, y, 0) * 5, Color.red);
                    if (hit)
                    {
                        targetTrans = hit.collider.gameObject.transform;
                    }
                }
            }
            trackTimer -= Time.deltaTime;
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Damage(10);
            Destroy(gameObject);
        }
    }
}
