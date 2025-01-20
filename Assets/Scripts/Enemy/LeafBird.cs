using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBird : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //�p�G���a���V����A���N���k�]�A���k�h����
        if(player.GetComponent<PlayerCtrl>().moveVector.x <= 0)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position +
              new Vector3(2f, 2f, 0), speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.GetComponent<PlayerCtrl>().moveVector.x >= 0)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position +
              new Vector3(-2f, 2f, 0), speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
        }
       
    }

    void Update()
    {
        
    }
}
