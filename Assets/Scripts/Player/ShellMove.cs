using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    //public GameObject Explosion;
    
    public float lifeTime;
    float timeLeft;
    public AudioSource audioSoure;
    
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= 1 * Time.deltaTime;
        if (timeLeft <= 0 )
        {
            Destroy(gameObject);
            audioSoure.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            audioSoure.Play();
        }
    }
}
