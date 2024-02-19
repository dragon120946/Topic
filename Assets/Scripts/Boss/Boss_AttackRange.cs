using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�p�G�i�J�����d��A�h���໷�Z�����A�p�G�W�X�h�i���Z����
public class Boss_AttackRange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<Boss>().canFarAttack = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<Boss>().canFarAttack = true;
        }
    }
}
