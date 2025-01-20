using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int hp;
    public int attack;
    public float maxTime = 0.5f;
    private float timer;

    private bool isHurt;
    public void Enemy_Update()
    {
        if (isHurt)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                isHurt = false;
                timer = 0;
            }

        }
    
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerCtrl>().Damage(attack);
        }
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "WaterBall(Clone)")
        {
            hp -= 5;
            isHurt = true;
        }
    }
}
