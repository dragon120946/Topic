using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
   public int hp;
   public int attack;

   public void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         other.gameObject.GetComponent<PlayerCtrl>().Damage(attack);
      }
      if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "WaterBall(Clone)")
      {
          hp -= 5;
      }
   }
}
